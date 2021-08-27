FROM python:3
WORKDIR /home/pi/Project
COPY . .
RUN pip3 install paho-mqtt
RUN pip3 install pymongo
CMD ["intercepter.py"]
ENTRYPOINT ["python3"]
