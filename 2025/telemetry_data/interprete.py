import json
import pandas as pd
import matplotlib.pyplot as plt
from datetime import datetime

# Cargar datos desde un JSON plano
import json

def cargar_bloques_json_multilinea(ruta):
    eventos = []
    bloque = ""
    with open(ruta, 'r', encoding='utf-8') as f:
        for linea in f:
            linea = linea.strip()
            if not linea:
                continue
            bloque += linea
            if linea.endswith('}'):  # posible final de un objeto
                try:
                    eventos.append(json.loads(bloque))
                    bloque = ""
                except json.JSONDecodeError:
                    bloque += " "  # sigue acumulando líneas si no se ha cerrado aún
    return eventos

# Convertir los eventos en un DataFrame
def procesar_eventos(eventos):
    df = pd.DataFrame(eventos)
    df['TimeStamp'] = pd.to_datetime(df['TimeStamp'], unit='s')  # Convertir a fecha legible
    return df

# Análisis y gráficas
def generar_graficas(df):
    print("Eventos por tipo:")
    print(df['ID_Event_'].value_counts())
    df['ID_Event_'].value_counts().plot(kind='bar', title='Frecuencia de eventos')
    plt.xticks(rotation=45)
    plt.tight_layout()
    plt.show()

    if 'ID_Interactuable' in df.columns:
        interacciones = df[df['ID_Event_'] == 'INTERACTION']
        interacciones['ID_Interactuable'].value_counts().plot(kind='bar', title='Interacciones con elementos')
        plt.xticks(rotation=45)
        plt.tight_layout()
        plt.show()

    if 'ID_Level' in df.columns:
        niveles = df[df['ID_Event_'].str.contains('LEVEL')]
        print("Eventos de nivel:")
        print(niveles)

    # Duración de la sesión
    start = df[df['ID_Event_'] == 'SESSION_START']['TimeStamp'].min()
    end = df[df['ID_Event_'] == 'SESSION_END']['TimeStamp'].max()
    duracion = (end - start).total_seconds()
    print(f"Duración total de la sesión: {duracion:.2f} segundos")


datos = cargar_bloques_json_multilinea("./2025_0.json")
df = procesar_eventos(datos)
generar_graficas(df)
