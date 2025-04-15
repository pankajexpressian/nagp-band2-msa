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

____________________________________________________________________________________

Application - 

	Product Service - 
		Web API Application written in DotNet 8.
		Has a few endpoints working on static data.
		Exposes the port 2001 on localhost
		
	Review Service - 
		Web API Application written in DotNet 8.
		Has a few endpoints working on static data.
		Exposes the port 2002 on localhost
		
	Api Gateway - 
		Web API Application written in DotNet 8
		No controllers, no endpoints.
		Ocelot is being used as API Gateway
		Exposes the port 2000 on localhost
		Nuget - <PackageReference Include="Ocelot" Version="19.0.4" />


DEMO-1: API Gateway
		
	Features - 
		a. API Gateway Implementation using Ocelot
		b. Composer/Aggregator Pattern Implementation 
	C:\Users\pankajjangid\source\repos\nagp-band2-msa\src\api_gateway

DEMO-2: Service Discovery
		a. Service registration through Eureka
		b. Service deregistration automatically when an instance goes down
		c. Service reregistration when new instance is started 
		d. Load balancing when multiple instance of a service is running
	C:\Users\pankajjangid\source\repos\nagp-band2-msa\src\service_discovery
	

DEMO-3: Microservice Communication
	Features:
		a. Sync communication using HTTP Client
		b. Async communication using Rabbit MQ Messaging
C:\Users\pankajjangid\source\repos\nagp-band2-msa\src\communication![image](https://github.com/user-attachments/assets/f694be5b-f07d-4eea-95ba-ffc5109cdad2)

