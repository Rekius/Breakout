BREAKOUT — WALL BOUNCE / ARCADE TINK V1
=======================================
Archivos: 3 variantes WAV mono, 48 kHz, PCM 16-bit.
Duración de cada rebote: 0.22 segundos (la cola audible es más corta).

Sonido: TINK electrónico, brillante, corto y sutil.
Diseñado para paredes, NO paleta: la paleta debe sonar con más cuerpo y los ladrillos con más impacto.

Sugerencia de implementación Unity:
- Usá AudioSource.PlayOneShot(wallClips[Random.Range(0, wallClips.Length)]).
- Pitch suave entre 0.97 y 1.03 para evitar repetición.
- Volumen de paredes por debajo de paleta y ladrillos.
- Evitá reproducir dos impactos en un mismo frame.

Todos los efectos son síntesis original y se pueden usar en tu proyecto.
