ARCADE BREAKOUT — PERDER VIDA | WAV PACK
======================================
3 variantes (48 kHz, mono, PCM 16 bit) de sonido de perder una vida.
Duración: 0,86–0,96 segundos.
Se reproduce al perder la pelota; no es el sonido definitivo de Game Over.

UNITY
Importá los 3 WAV y asigná uno al azar cuando el jugador pierda una vida.
Usá AudioSource.PlayOneShot(clip, volumeScale);
Como punto de partida, volumeScale = 0.7f, ajustar a oído frente a ladrillos y rebotes.
Para evitar sumas desagradables, disparalo una única vez por vida perdida.
