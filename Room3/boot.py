import time
from umqttsimple import MQTTClient
import ubinascii
import machine
from machine import Pin, ADC
import dht
import micropython
import network
import ujson
import esp
import math
try:
  import usocket as socket
except:
  import socket
esp.osdebug(None)
import gc
gc.collect()

# network credentials

ssid = 'MiFibra-A792' 
# ssid = 'TP-LINK_6F4134'
password = 'yCZbTtk5' 
# password = '01470260'
mqtt_server = '192.168.1.85'
# mqtt_server = '192.168.0.202'

# define the machine ID
client_id = ubinascii.hexlify(machine.unique_id())

# publishing topics

topic_sub = [b'university/room2/temperature', b'university/room2/humidity', b'university/outside/temperature', b'university/outside/humidity'] # no subscription necessary
topic_pub_temperature = b'university/room3/temperature'
topic_pub_humidity = b'university/room3/humidity'
topic_pub_light = b'university/room3/light'
topic_pub_gas = b'university/room3/gas'
topic_pub_h2 = b'university/room3/h2'
topic_pub_lpg = b'university/room3/lpg'
topic_pub_ch4 = b'university/room3/ch4'
topic_pub_co = b'university/room3/co'
topic_pub_alc = b'university/room3/alcohol'
topic_pub_smoke = b'university/room3/smoke'
topic_pub_c3h8 = b'university/room3/c3h8'

# set the message interval variables

last_message = 0
message_interval = 9
counter = 0

# connect to the network

station = network.WLAN(network.STA_IF)

station.active(True)
station.connect(ssid, password)

while station.isconnected() == False:
  pass

print('Connection successful')
print(station.ifconfig())
DHTsensor = dht.DHT22(Pin(32, Pin.IN, Pin.PULL_UP))
light_sensor = ADC(Pin(34))
smoke_sensor = ADC(Pin(35))