def sub_cb(topic, msg):
  data_topic = str(topic)[2:-1]
  data_message = str(msg)[2:-1]
  # data_topic_split = data_topic.split('/')
  # data_message_split = data_message.split('/')
  convertable_data = {
    "topic": data_topic,
    "message": data_message
  }
  json_data = ujson.dumps(convertable_data)
  # with open("sensors_data.json", "a+") as outfile:
  #  ujson.dump(json_data, outfile)
  # with open("sensors_data.json", "r") as file:
  #   data = file.read()
  #print('Topic: %s     Message: %s' % (data_topic, data_message))

def connect_and_subscribe():
  global client_id, mqtt_server, topic_sub
  client = MQTTClient(client_id, mqtt_server)
  client.set_callback(sub_cb)
  client.connect()
  for i in range(len(topic_sub)):
    client.subscribe(topic_sub[i])
    #print('Connected to %s MQTT broker, subscribed to %s topic' % (mqtt_server, topic_sub[i]))
  return client

def restart_and_reconnect():
  print('Failed to connect to MQTT broker. Reconnecting...')
  time.sleep(10)
  machine.reset()

try:
  client = connect_and_subscribe()
except OSError as e:
  restart_and_reconnect()

counter = 0

while True:
  try:
    client.check_msg()
    if (time.time() - last_message) > message_interval:

      # data computation

      DHTsensor.measure()
      temp = DHTsensor.temperature()
      hum = DHTsensor.humidity()
      light = light_sensor.read() / 1024 * 100
      light_lux = light_sensor.read() * 0.9765625

      # messages to be sent to the broker

      msg_temperature = b'Temperature: %.2f *C' % temp
     # print(temp)
      msg_humidity = b'Humidity: %.2f %%' % hum
     # print(hum)
      msg_light = b'Light intensity: %.2f %% (%.2f lux)' % (light, light_lux)

      # publishing the messages

      client.publish(topic_pub_temperature, msg_temperature)
      client.publish(topic_pub_humidity, msg_humidity)
      client.publish(topic_pub_light, msg_light)
      
      last_message = time.time()
  except OSError as e:
    restart_and_reconnect()