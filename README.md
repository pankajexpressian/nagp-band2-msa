# nagp-band2-msa

## Run RabbitMQ Container
docker run -d --hostname rabbitmq-host --name rabbitmq-container -p 5672:5672 -p 15672:15672 rabbitmq:3-management

localhost:15672

_____________________________________________________

## Run Eureka Container

docker run -d steeltoeoss/eureka-server -p 8761:8761
or
docker run -d --name eureka -p 8761:8761 steeltoeoss/eureka-server

http://localhost:8761/
_____________________________________________________


## Services and APi Gateway

product service - http://localhost:2001
review service - http://localhost:2002
ocelot service - http://localhost:2000

product service instance 2 - http://localhost:2003
