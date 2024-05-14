# Pi-User.Api.

# Setup to run project

1.Download and install docker desktop  
2.Open project , set docker-compose as Startup Project  
3.Run docker-compose and Swagger should shown up  


# If you encouter with error about migrate database while running the project

1.Open Commmand Prompt as an Administrator  
2.Type the command: 'netstat -ano | findstr :3306'  
3.Look for the line that has :3306 in it. At the end of that line, you will see the PID of the process using the port.  
4.Still in Command Prompt, type: 'taskkill /PID <PID> /F'  
5.Replace <PID> with the actual PID number you found from the first step. example => taskkill /PID 5644 /F

# How to call API

1. First, get access token by call httpPost at https://localhost:8081/Login with body
{
    "Username" : "admin",
    "Password" : "P@ssw0rd"
}
2. Use access token from response to add bearer token to call CRUD api

# Decisions and trade-offs
In a limited time, I chose to do integration testing and an API structure that was easy to use.
and can expand the size of the app in the future. I decided to use serilog for its ability to write logs and 
provide an easy-to-use log structure, and chose to use mysql via docker compose for ease of use on other machines. 
For testing, I used in- memory database to cause minimal impact on the main database's data. 
And if I had more time, What I might do is
- Redis caching server
- Improve log structure
- Pipeline code coverage
- Custom exception