import os, sys, json
import pandas as pd
import numpy as np
import matplotlib.pyplot as plt

# 0) Cargamos todos los JSON

def cargar_todos_los_json():
    eventos = []
    archivos = [f for f in os.listdir('.') if f.endswith('.json')]
    for arc in archivos:
        with open(arc, 'r', encoding='utf-8') as file:
            bloque = ''
            for lin in file:
                lin = lin.strip()
                if not lin:
                    continue
                bloque += lin
                if lin.endswith('}'):
                    try:
                        eventos.append(json.loads(bloque))
                        bloque = ''
                    except:
                        bloque += ' '
    print(f"He leído {len(archivos)} JSONs")
    return eventos

# 1) Cargamos datos
eventos = cargar_todos_los_json()
if not eventos:
    print("No se cargó ninguno, asegúrate de que haya archivos .json")
    sys.exit(1)

df = pd.DataFrame(eventos)
if 'TimeStamp' not in df.columns:
    print("Falta la columna TimeStamp")
    sys.exit(1)
try:
    df['TimeStamp'] = pd.to_datetime(df['TimeStamp'], unit='s')
except Exception as e:
    print("Error convirtiendo TimeStamp:", e)
    sys.exit(1)

# 2) Gráfico de usos de arma e impactos
if {'ID_Event_', 'ID_Weapon'}.issubset(df.columns):
    ataques = df[df['ID_Event_'] == 'ATTACK']
    hits = df[df['ID_Event_'] == 'HIT']
    # Contamos usos de Pistola y Palanca
    counts_attack = ataques['ID_Weapon'].value_counts()
    pistol_uses = counts_attack.get('Pistola', 0)
    crowbar_uses = counts_attack.get('Palanca', 0)
    # Contamos impactos de bala y palanca
    counts_hits = hits['ID_Weapon'].value_counts()
    bullet_hits = counts_hits.get('Pistol_Bullet(Clone)', 0)
    crowbar_hits = counts_hits.get('Crowbar_Dash(Clone)', 0)
    # Preparamos gráfico
    labels = ['Pistola', 'Palanca', 'impacto de bala', 'impacto de palanca']
    values = [pistol_uses, crowbar_uses, bullet_hits, crowbar_hits]
    colors = ['blue', 'blue', 'red', 'red']
    plt.figure(figsize=(6, 4))
    plt.bar(labels, values, color=colors)
    plt.title('Usos de armas e impactos')
    plt.ylabel('Cantidad')
    plt.tight_layout()
    plt.show()
else:
    print("No existe lo necesario para el gráfico de armas e impactos")

# 3) Gráfico vida curada vs daño recibido
healing = df[df['ID_Event_'] == 'HEALTH_UP']['healing_amount'].sum() if 'healing_amount' in df.columns else 0
damage_received = df[df['ID_Event_'] == 'DAMAGE_RECIEVED']['damage'].sum() if 'damage' in df.columns else 0
if healing or damage_received:
    plt.figure(figsize=(6, 4))
    plt.bar(['Vida curada', 'Daño recibido'], [healing, damage_received])
    plt.title('Vida curada vs Daño recibido')
    plt.ylabel('Totales')
    plt.tight_layout()
    plt.show()
else:
    print("Falta data de curas o daños")

# 4) Gráfico de daño a lo largo del tiempo relativo
if {'ID_Event_', 'TimeStamp', 'ID_Session', 'damage'}.issubset(df.columns):
    ses_start = df[df['ID_Event_'] == 'SESSION_START'].set_index('ID_Session')['TimeStamp']
    ses_end = df[df['ID_Event_'] == 'SESSION_END'].set_index('ID_Session')['TimeStamp']
    último = df.groupby('ID_Session')['TimeStamp'].max()
    ses_end = ses_end.reindex(ses_start.index).fillna(último)
    dam = df[df['ID_Event_'] == 'DAMAGE_RECIEVED'].copy()
    dam['rel_time'] = dam.apply(lambda r: (r['TimeStamp'] - ses_start[r['ID_Session']]).total_seconds(), axis=1)
    max_dur = (ses_end - ses_start).dt.total_seconds().max()
    bins = np.arange(0, max_dur + 10, 10)
    counts, edges = np.histogram(dam['rel_time'], bins=bins)
    plt.figure(figsize=(6, 4))
    plt.bar(edges[:-1], counts, width=10)
    plt.title('Daño recibido a lo largo del tiempo')
    plt.xlabel('Segundos desde inicio de sesión')
    plt.xlim(0, max_dur)
    plt.ylabel('Eventos')
    plt.tight_layout()
    plt.show()
else:
    print("Faltan columnas para el gráfico de daño")

# 5) Tiempo medio y gráfico para recoger cada fusible desde SESSION_START
if {'ID_Event_', 'ID_Interactuable', 'TimeStamp', 'ID_Session'}.issubset(df.columns):
    fus = df[(df['ID_Event_'] == 'INTERACTION') & df['ID_Interactuable'].str.contains('Fusible', na=False)].copy()
    if not fus.empty:
        ses_start = df[df['ID_Event_'] == 'SESSION_START'].set_index('ID_Session')['TimeStamp']
        fus['orden'] = fus.groupby('ID_Session').cumcount() + 1
        fus['rel_time'] = fus.apply(lambda r: (r['TimeStamp'] - ses_start[r['ID_Session']]).total_seconds(), axis=1)
        tiempos = fus.pivot(index='ID_Session', columns='orden', values='rel_time')
        media = tiempos.mean()
        # Imprimimos tiempos medios
        for orden, t in media.items():
            print(f"Tiempo medio fusible{int(orden)}: {t:.2f}s")
        # Gráfico de barras de fusibles
        plt.figure(figsize=(6, 4))
        labels_f = [f"fusible{int(i)}" for i in media.index]
        plt.bar(labels_f, media.values)
        plt.title('Tiempo medio por fusible')
        plt.xlabel('Fusible')
        plt.ylabel('Segundos')
        plt.tight_layout()
        plt.show()
    else:
        print("No detecté ningún fusible")
else:
    print("Faltan datos para calcular tiempo medio de fusibles")

# 6) Métricas finales
if 'ID_Session' in df.columns:
    med_fus = fus.groupby('ID_Session').size().mean() if 'fus' in locals() else 0
    med_att = ataques.groupby('ID_Session').size().mean() if 'ataques' in locals() else 0
    comp = df[(df['ID_Event_'] == 'INTERACTION') & (df['ID_Interactuable'] == 'Electricidad')]['ID_Session'].nunique()
    total = df['ID_Session'].nunique()
    pct = comp / total * 100 if total > 0 else 0
    if 'LEVEL_START' in df['ID_Event_'].unique():
        inicio = df[df['ID_Event_'] == 'LEVEL_START'].groupby('ID_Session')['TimeStamp'].min()
    else:
        inicio = pd.Series(dtype='float64')
    if 'SESSION_END' in df['ID_Event_'].unique():
        fin = df[df['ID_Event_'] == 'SESSION_END'].groupby('ID_Session')['TimeStamp'].max()
    else:
        fin = pd.Series(dtype='float64')
    tj = (fin - inicio).dropna().dt.total_seconds()
    tmedio = tj.mean() if not tj.empty else 0
    print(f"media fusibles x sesion: {med_fus:.2f}")
    print(f"media ataques x sesion: {med_att:.2f}")
    print(f"% q completan lvl1: {pct:.2f}%")
    print(f"tiempo medio hasta lvl1 o cierre: {tmedio:.2f}s")
else:
    print("No se pueden sacar métricas finales, falta ID_Session")
