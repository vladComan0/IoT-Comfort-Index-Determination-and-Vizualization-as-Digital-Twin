from datetime import datetime, timedelta
import time
import paho.mqtt.client as paho
from pymongo import MongoClient
from collections import OrderedDict


broker = "192.168.1.22"
temperature = "0"
humidity = "0"
light_intensity = "0"
light_intensity_lux = 0


def on_message(client, userdata, message):
   time.sleep(1)
   global temperature
   global humidity
   global light_intensity
   global light_intensity_lux
   data = str(message.payload.decode("utf-8"))
   topic = message.topic
   place = topic.split('/')[1]
   if data.split(' ')[0] == 'Temperature:':
      temperature = data.split(' ')[1]
   elif data.split(' ')[0] == 'Humidity:':
      humidity = data.split(' ')[1]
   elif data.split(' ')[0] == 'Light' and data.split(' ')[1] == 'intensity:':
      light_intensity = data.split(' ')[2]
      light_intensity_lux = float(light_intensity) * 10.24 * 0.9765625
   fatigue = -0.021 * light_intensity_lux + 35.17  # input website here
   comfort = float(temperature) - 0.55 * (1-0.01 * float(humidity)) * (float(temperature) - 14.5)  # keisan.casio.com
   convertable_data = OrderedDict([
      ("Place", place),
      ("Temperature", temperature + u' \N{DEGREE SIGN}C'),
      ("Humidity", humidity + ' %'),
      ("Light Intensity", light_intensity + ' %'),
      ("Fatigue Index", str(fatigue)[0:5] + ' %'),
      ("Comfort Index", str(comfort)[0:5]),
      ("Time", str(time_now))
   ])
   mongo_client = MongoClient("147.156.85.248", 27017, username='Admin', password='admin1234', authSource='CloudDB')
   db = mongo_client["CloudDB"]
   if place == 'room1':
      db.CloudDB.delete_one({"Place": "room1"})
      post_id = db.CloudDB.insert_one(convertable_data).inserted_id


client = paho.Client(client_id="client-001")
client.username_pw_set('Room1', '1234')
client.connect(broker)  # connect to the broker
client.loop_start()  # start loop to process the received messages

while True:
   time_now = datetime.now() + timedelta(hours=2)
   time_now = time_now.strftime("%H:%M:%S")
   client.on_message = on_message
   client.subscribe("university/room1/temperature")
   client.subscribe("university/room1/humidity")
   client.subscribe("university/room1/light")
   client.subscribe("university/room1/gas")
   time.sleep(1)
