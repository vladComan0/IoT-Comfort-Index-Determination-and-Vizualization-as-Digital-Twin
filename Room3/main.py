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
  print("Here 1")
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
      light = light_sensor.read() / 4095 * 100
      light_lux = light_sensor.read() * 0.9765625
      print('Air temperature: {}°C ' .format(temp))
      print('Air humidity: {0:.2f}% ' .format(hum))
      print('Light intensity: {0:.2f}% ' .format(light))
      try:
        vout = smoke_sensor.read() * 3.3 / 4095
      except ZeroDivisionError:
         vout = 3.3
      rs = (5 - vout) / vout
      rat = rs/10000
      #x = 10 ^ {[log(y) - b] / m}
      #https://thestempedia.com/tutorials/interfacing-mq-2-gas-sensor-with-evive/#Calculating_PPM_for_a_particular_gas
      h2 = 10** ( (math.log(rs) - 1.412572126) / (-0.47305447) )
      lpg = 10** ( (math.log(rs) - 1.25063406) / (-0.454838059) )
      ch4 = 10** ( (math.log(rs) - 1.349158571) / (-0.372003751) )
      co = 10** ( (math.log(rs) - 1.512022272) / (-0.33975668) )
      alc = 10** ( (math.log(rs) - 1.310286169) / (-0.373311285) )
      smoke = 10** ( (math.log(rs) -	1.617856412) / (-0.44340257) )
      c3h8 = 10** ( (math.log(rs) -	1.290828982) / (-0.461038681) )

      # messages to be sent to the broker

      msg_temperature = b'Temperature: %.2f *C' % temp
     # print(temp)
      msg_humidity = b'Humidity: %.2f %%' % hum
     # print(hum)
      msg_light = b'Light intensity: %.2f %% (%.2f lux)' % (light, light_lux)

      #gas sensor
      msg_h2 = b'H2 concentration: %.2f ppm' % h2
      msg_lpg = b'LPG concentration: %.2f ppm' % lpg
      msg_ch4 = b'Methane concentration: %.2f ppm' % ch4
      msg_co = b'CO concentration: %.2f ppm' % co
      msg_alc = b'Alcohol concentration: %.2f ppm' % alc
      msg_smoke = b'Smoke concentration: %.2f ppm' % smoke
      msg_c3h8 = b'Propane concentration: %.2f ppm' % c3h8

      print('h2 concentration: {}ppm' .format(h2))
      print('LPG concentration: {}ppm' .format(lpg))
      print('Methane concentration: {}ppm' .format(ch4))
      print('CO concentration: {}ppm' .format(co))
      print('Alcohol concentration: {}ppm' .format(alc))
      print('Smoke concentration: {}ppm' .format(smoke))
      print('Propane concentration: {}ppm' .format(c3h8))
      print('\n')
      # publishing the messages

      client.publish(topic_pub_temperature, msg_temperature)
      client.publish(topic_pub_humidity, msg_humidity)
      client.publish(topic_pub_light, msg_light)
      client.publish(topic_pub_h2, msg_h2)
      client.publish(topic_pub_lpg, msg_lpg)
      client.publish(topic_pub_ch4, msg_ch4)
      client.publish(topic_pub_co, msg_co)
      client.publish(topic_pub_alc, msg_alc)
      client.publish(topic_pub_smoke, msg_smoke)
      client.publish(topic_pub_c3h8, msg_c3h8)
      
      last_message = time.time()
  except OSError as e:
    print("Here 2")
    #restart_and_reconnect()