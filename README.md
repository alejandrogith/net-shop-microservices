# Proyect Management

It consists of a backend application based on microservices architecture.
It allows managing the sale of products through orders.

## Microservices

- Api Gateway microservice 1
- Order microservice 2
- Product microservice 3
- Inventory microservice 4
- Customer-Auth microservice 5

### Features

- Security Management with Authorization and Authentication through Login and Registration using JWT and Asp.net Identity 6
- API documentation with Swagger
- Database management with Entity Framework core
- Data persistence using SQL Server
- Advanced pagination
- Order Management.
- Product Management.
- Inventory Management.
- Customer Management.
- Microservice discovery and registration Apis with Consul Service Discovery.
- URL routing with Ocelot Api Gateway.

### Technology tools

- Ocelot Api Gateway
- Consul Service Discovery
- Asp.net 6
- Entity Framework Core 6
- Asp.net Identity 6
- C# 11
- Nuget dependency manager

### Getting started

- Install or download the Docker image from Consul

```bash
# Docker image
$ docker pull consul

# Run the service
$ docker run -d -p 8500:8500 -p 8600:8600/udp --name=my-consul consul agent -server -ui -node=server-1 -bootstrap-expect=1 -client=0.0.0.0
```

- Then raise all the microservices starting with the Api Gateway microservice to consume the service discovery and trigger routing through the same URL.
