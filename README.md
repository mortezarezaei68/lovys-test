# Lovys Technical Assignment
## Build an interview calendar API



Tech stack used : ASP.NET Core 3.1, C# 9.0 and SqlServer and ASP.NET Identity and Entity Framework Core 3.1

## Features

- created and managed user with identity library
- get and set user access token and refresh token and save by HttpOnlyCookie
- persisted grant in every request to get token
- set and managed interviewer and candidate calender booking
- used eventbus and mediatR library to simplfy communication between commands and api service


This text you see here is *actually- written in Markdown! To get a feel
for Markdown's syntax, type some text into the left window and
watch the results in the right.

## running in developing software like visual studio, rider, vs code

if you want to run with Visual studio or rider you have to set your appsettings.Development.json below parts:
this code for set connection string to connect to your sql server database
```
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=localhost,1433;Initial Catalog=InterviewApplication;User Id=sa;Password=@Daneshgah65411887;Integrated Security=false;"
  },
```
if you want to manage this application in different domain you have to set "DomainUrl" because application have to set cookie in your browser. "AccessTokenExpiredTime" is for access token expired date time and "ExpirationDays"  is for refresh token expiration and if you wanted to use jwe you can enable "SecretKey" and "EncryptedKey" used to decrypt and encrypt for the access token
```
 "JwtToken": {
    "SecretKey": "x8{'5,C4ram)5zLq",
    "EncryptedKey": "ax7@S>QgDkEW=$6M",
    "Issuer": "lovys",
    "Audience": "lovys.Web",
    "AccessTokenExpiredTime": "604800",
    "ExpirationDays": "30",
    "DomainUrl": "localhost"
  }
```

you should set cors origin in Startup.cs
## Docker
you can build and running application in docker with this command in terminal or dos:
    ```
    docker-compose up --build
    ```
## API description
| api | description | type |
| :---: | :---: | :---: |
| /api/v1/Schedule/current-user-schedule | get current user calendar days | GET |
| /api/v1/Schedule/interviewer-schedule | get intersect of interviewers and candidates | GET |
| /api/v1/User | get all users list | GET |
| /api/v1/Schedule | create schedule date.you can see example in notice part | POST |
| /api/v1/User | to create user with user type 1 is interviewer and 0 is condidate | POST |
| /api/v1/User/login-user | login user and set token in browser cookie | POST |

## Notice

when you want to use `/api/v1/Schedule` api to create a booking datatime you should use this format:
```
{
  "bookingDate": "2021-10-19",
  "startedTime": "10:00:00",
  "endedTime": "09:00:00"
}
```

the response of `/api/v1/Schedule/interviewer-schedule` api is like this for Mary and Diana and John intersection:
```
{
  "data": [
    {
      "firstName": "Mary",
      "lastName": "Mary",
      "startedDate": "10:00:00",
      "endedDate": "09:00:00",
      "bookingDate": "2021-10-26T00:00:00",
      "candidateSchedule": [
        {
          "firstName": "John",
          "lastName": "John",
          "startedDate": "10:00:00",
          "endedDate": "09:00:00"
        }
      ]
    },
    {
      "firstName": "Mary",
      "lastName": "Mary",
      "startedDate": "10:00:00",
      "endedDate": "09:00:00",
      "bookingDate": "2021-10-28T00:00:00",
      "candidateSchedule": [
        {
          "firstName": "John",
          "lastName": "John",
          "startedDate": "10:00:00",
          "endedDate": "09:00:00"
        }
      ]
    },
    {
      "firstName": "Diana",
      "lastName": "Diana",
      "startedDate": "10:00:00",
      "endedDate": "09:00:00",
      "bookingDate": "2021-10-26T00:00:00",
      "candidateSchedule": [
        {
          "firstName": "John",
          "lastName": "John",
          "startedDate": "10:00:00",
          "endedDate": "09:00:00"
        }
      ]
    },
    {
      "firstName": "Diana",
      "lastName": "Diana",
      "startedDate": "10:00:00",
      "endedDate": "09:00:00",
      "bookingDate": "2021-10-28T00:00:00",
      "candidateSchedule": [
        {
          "firstName": "John",
          "lastName": "John",
          "startedDate": "10:00:00",
          "endedDate": "09:00:00"
        }
      ]
    }
  ],
  "count": 4,
  "isSuccess": true,
  "message": null
}
```
this is your answer









