# -*- coding: utf-8 -*-
"""
Created on Fri Mar  3 21:17:12 2023

@author: AXGNO01
"""

# -*- coding: utf-8 -*-
"""
Created on Fri Mar  3 21:06:02 2023

@author: Gabriel Nobel
"""
import matplotlib.pyplot as plt
import pandas as pd

df = pd.read_csv("./time_series_z.csv")
df = df[df['t'] <= 20];
print(df)

x = df['t'];

plt.figure(1)
plt.plot(x, df['x(t)'])
plt.xlabel('Time t in seconds [s]')
plt.ylabel('Place coordinates z(t)')
plt.show()

plt.figure(2)
plt.plot(x, df['v(t)'])
plt.xlabel('Time t in seconds [s]')
plt.ylabel('Velocity in km/h v(t)')
plt.show()

plt.figure(3)
plt.plot(x, df['F(t) (added)'])
plt.xlabel('Time t in seconds [s]')
plt.ylabel('Force in Newton F(t)')
plt.show()


