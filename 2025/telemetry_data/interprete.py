import os
import sys
import json
import pandas as pd
import matplotlib.pyplot as plt

# Leer todos los archivos JSON en el directorio actual
def cargar_todos_los_json():
    eventos = []
    archivos = [f for f in os.listdir('.') if f.endswith('.json')]
    for archivo in archivos:
        with open(archivo, 'r', encoding='utf-8') as f:
            bloque = ''
            for linea in f:
                linea = linea.strip()
                if not linea:
                    continue
                bloque += linea
                if linea.endswith('}'):
                    try:
                        eventos.append(json.loads(bloque))
                        bloque = ''
                    except json.JSONDecodeError:
                        bloque += ' '
    print(f"Se han leído correctamente {len(archivos)} archivos JSON.")
    return eventos

# Cargar y verificar datos
eventos = cargar_todos_los_json()
if not eventos:
    print("No se cargaron eventos JSON. Asegúrate de que hay archivos .json en el directorio actual.")
    sys.exit(1)

df = pd.DataFrame(eventos)

# Validar columna TimeStamp
if 'TimeStamp' not in df.columns:
    print("Error: No se encontró la columna 'TimeStamp' en los datos cargados.")
    sys.exit(1)
# Convertir TimeStamp de segundos a datetime
try:
    df['TimeStamp'] = pd.to_datetime(df['TimeStamp'], unit='s')
except Exception as e:
    print(f"Error al convertir 'TimeStamp': {e}")
    sys.exit(1)

# Gráfica 1: Cantidad de ATTACK y su división
if 'ID_Event_' in df.columns and 'ID_Weapon' in df.columns:
    attack_df = df[df['ID_Event_'] == 'ATTACK']
    if not attack_df.empty:
        arma_counts = attack_df['ID_Weapon'].value_counts()
        plt.figure(figsize=(6, 4))
        arma_counts.plot(kind='bar', title='Uso de armas en ATTACK')
        plt.ylabel('Cantidad')
        plt.xticks(rotation=0)
        plt.tight_layout()
        plt.show()
    else:
        print("No hay eventos ATTACK para graficar.")
else:
    print("No se puede graficar uso de armas: falta la columna 'ID_Weapon' o 'ID_Event_'.")

# Gráfica 2: Vida curada vs daño recibido
curacion = df['value'].sum() if 'value' in df.columns and 'ID_Event_' in df.columns else 0
danio = df['damage'].sum() if 'damage' in df.columns and 'ID_Event_' in df.columns else 0
if curacion or danio:
    plt.figure(figsize=(6, 4))
    plt.bar(['Vida curada', 'Daño recibido'], [curacion, danio])
    plt.title('Vida Curada vs Daño Recibido')
    plt.ylabel('Cantidad')
    plt.tight_layout()
    plt.show()
else:
    print("No hay datos de curación o daño para graficar.")

# Gráfica 3: Eventos de daño según timestamp
if 'ID_Event_' in df.columns and 'TimeStamp' in df.columns and 'damage' in df.columns:
    damage_over_time = df[df['ID_Event_'] == 'DAMAGE_RECIEVED'].set_index('TimeStamp')
    if not damage_over_time.empty:
        damage_over_time.resample('10S').size().plot(title='Eventos de daño a lo largo del tiempo')
        plt.ylabel('Número de eventos')
        plt.tight_layout()
        plt.show()
    else:
        print("No hay eventos de daño para graficar en el tiempo.")
else:
    print("No se puede graficar eventos de daño: faltan columnas necesarias.")

# Gráfica 4: Tiempo medio en recoger cada fusible
if 'ID_Event_' in df.columns and 'ID_Interactuable' in df.columns and 'TimeStamp' in df.columns and 'ID_Session' in df.columns:
    fusibles = df[(df['ID_Event_'] == 'INTERACTION') & (df['ID_Interactuable'].str.contains('Fusible', na=False))].copy()
    if not fusibles.empty:
        fusibles['orden'] = fusibles.groupby('ID_Session').cumcount() + 1
        tiempos_fusibles = fusibles.groupby(['ID_Session', 'orden'])['TimeStamp'].min().unstack()
        tiempos_fusibles = tiempos_fusibles.subtract(tiempos_fusibles.min(axis=1), axis=0)
        tiempos_fusibles.mean().plot(kind='bar', title='Tiempo medio en recoger cada fusible')
        plt.ylabel('Tiempo (s)')
        plt.tight_layout()
        plt.show()
    else:
        print("No se detectaron interacciones con fusibles.")
else:
    print("No se puede calcular tiempos de fusibles: faltan columnas necesarias.")

# Gráfica 5: Recolección de notas, munición y botiquines
if 'ID_Event_' in df.columns and 'ID_Interactuable' in df.columns:
    conteo_elementos = df[df['ID_Event_'] == 'INTERACTION']['ID_Interactuable'].value_counts()
    recolectables = ['NotaPrincipal', 'Municion', 'Botiquin']
    conteo_recolectables = conteo_elementos[conteo_elementos.index.isin(recolectables)]
    if not conteo_recolectables.empty:
        conteo_recolectables.plot(kind='bar', title='Elementos recogidos')
        plt.ylabel('Cantidad')
        plt.xticks(rotation=0)
        plt.tight_layout()
        plt.show()
    else:
        print("No hay recolectables para graficar.")
else:
    print("No se puede graficar recolectables: faltan columnas necesarias.")

# Métricas finales
if 'ID_Session' in df.columns:
    # Media de recolección de fusibles
    media_fusibles = fusibles.groupby('ID_Session').size().mean() if 'fusibles' in locals() and not fusibles.empty else 0
    # Media de uso de armas
    media_ataques = attack_df.groupby('ID_Session').size().mean() if 'attack_df' in locals() and not attack_df.empty else 0
    # Porcentaje de jugadores que completan el nivel 1 (interactúan con "Electricidad")
    completan = df[(df['ID_Event_'] == 'INTERACTION') & (df['ID_Interactuable'] == 'Electricidad')]['ID_Session'].nunique()
    total_jugadores = df['ID_Session'].nunique()
    porcentaje_completan = completan / total_jugadores * 100 if total_jugadores > 0 else 0
    # Tiempo medio en completar nivel 1 o cerrar sesión
    nivel1_start = df[df['ID_Event_'] == 'LEVEL_START'].groupby('ID_Session')['TimeStamp'].min() if 'LEVEL_START' in df['ID_Event_'].unique() else pd.Series(dtype='float64')
    fin_sesion = df[df['ID_Event_'] == 'SESSION_END'].groupby('ID_Session')['TimeStamp'].max() if 'SESSION_END' in df['ID_Event_'].unique() else pd.Series(dtype='float64')
    tiempos_juego = (fin_sesion - nivel1_start).dropna().dt.total_seconds()
    tiempo_medio = tiempos_juego.mean() if not tiempos_juego.empty else 0

    print(f"Media de recolección de fusibles por sesión: {media_fusibles:.2f}")
    print(f"Media de ataques por sesión: {media_ataques:.2f}")
    print(f"Porcentaje de jugadores que completan el nivel 1: {porcentaje_completan:.2f}%")
    print(f"Tiempo medio de juego hasta nivel 1 o cierre: {tiempo_medio:.2f} segundos")
else:
    print("No se pueden calcular métricas finales: falta la columna 'ID_Session'.")