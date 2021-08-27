var mqtt;
var reconnectTimeout = 20;
var host;
var port = 9001;
var username;
var temperature = 0;
var humidity = 0;
var light_intensity = 0;
$(document).ready(function(){
    $("#rooms").change(function(){
        if($(this).val() == "room1"){
            host = "192.168.1.22";
            username="Room1";
            MQTTconnect();
        }
        if($(this).val() == "room2"){
            host = "192.168.1.115";
            username="Room2";
            MQTTconnect();
        }
        if($(this).val() == "room3"){
            host = "192.168.1.85";
            username="Room3";
            MQTTconnect();
        }
        if($(this).val() == "outside"){
            host = "192.168.1.62";
            username="Outside";
            MQTTconnect();
        }
    })
});

function onConnect(){
    console.log("Connected");
    mqtt.subscribe("university/" + username.toLowerCase() + "/temperature");
    mqtt.subscribe("university/" + username.toLowerCase() + "/humidity");
    mqtt.subscribe("university/" + username.toLowerCase() + "/light");
}

function MQTTconnect(){
    console.log("connecting to " + host + ":" + port);
    mqtt = new Paho.MQTT.Client(host, port, "clientjs");
    var options = {
        timeout: 3,
        userName: username,
        password: '1234',
        onSuccess: onConnect,
        onFailure: onFailure,
    };
    mqtt.onMessageArrived = onMessageArrived
    mqtt.connect(options)
}

function onFailure(){
    console.log("Connection attempt to " + host + ":" + port + " failed.");
    setTimeout(MQTTconnect, reconnectTimeout);
}

function onMessageArrived(message){
    topic = message.destinationName;
    data = message.payloadString;
    console.log(data);
    $("#place").html("<h5>Place: " + username + "</h5>");
    if(topic == ("university/" + username.toLowerCase() + "/temperature")){
        $("#temperature").html("<h5>" + data + "</h5>");
        temperature = data.split(" ")[1];
    }
    if(topic == ("university/" + username.toLowerCase() + "/humidity")){
        $("#humidity").html("<h5>" + data + "</h5>");
        humidity = data.split(" ")[1];
    }
    if(topic == ("university/" + username.toLowerCase() + "/light")){
        $("#light").html("<h5>" + data + "</h5>");
        light_intensity = data.split(" ")[2];
    }
    console.log("Temperature " + parseFloat(temperature));
    console.log("Humidity " + parseFloat(humidity));
    light_intensity_lux = parseFloat(light_intensity) * 1024 / 100.00 * 0.9765625;
    fatigue = (-0.021 * light_intensity_lux + 35.17).toFixed(2);
    comfort = (parseFloat(temperature) - 0.55 * (1 - 0.01 * parseFloat(humidity)) * (parseFloat(temperature) - 14.5)).toFixed(2);
    $("#fatigue-index").html("<h5>Fatigue Index: " + fatigue + " %</h5>");
    $("#comfort-index").html("<h5>Comfort Index: " + comfort + "</h5>");
}