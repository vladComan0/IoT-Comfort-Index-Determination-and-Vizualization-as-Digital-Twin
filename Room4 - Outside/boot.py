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
mqtt_server = '192.168.1.62'
# mqtt_server = '192.168.0.202'

# define the machine ID
client_id = ubinascii.hexlify(machine.unique_id())

# publishing topics

topic_sub = [b'university/room2/temperature', b'university/room2/humidity', b'university/outside/temperature', b'university/outside/humidity'] # no subscription necessary
topic_pub_temperature = b'university/outside/temperature'
topic_pub_humidity = b'university/outside/humidity'
topic_pub_light = b'university/outside/light'
topic_pub_moisture = b'university/outside/moisture'

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
moisture_sensor = ADC(Pin(35))