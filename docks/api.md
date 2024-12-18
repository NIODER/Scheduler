# API mapping

## Methods

- register
- login
- user
- group
- group/user
- settings/user
- invite/group
- invite/user
- task
- finance
- updates
- finance/calculate/fill
- finance/calculate/distribute

## Every response returns an standart container

#### requests are raw and responses are "response" field contaiment
```
{
    "time": "20:12:22",
    "error": {
        "message": "error message",
        "code": 123
    },
    "response": {
        "userId": "0000-0000-000000",
        "userName": "alexander"
    }
}
```

## Register

https://scheduler.com/authentication/register

### POST:

#### request
```
{
    "username": "alexander",
    "email": "alexander@gmail.com",
    "description": "description",
    "password": "password"
}
```
#### response
```
{
    "userId": "0000-0000-000000",
    "username": "alexander",
    "email": "alexander@gmail.com"
    "description": "description"
}
```

## Login

https://scheduler.com/authentication/login

### POST:

#### request
```
{
    "email": "alexander@gmail.com",
    "password": "password"
}
```
#### response
```
{
    "userId": "0000-0000-000000",
    "userName": "alexander",
    "email": "alexander@gmail.com"
}
```

## User

http://scheduler.com/user/{userId}

### GET:

#### response
```
{
    "userId": "0000-0000-000000",
    "userName": "alexander",
    "description": "description"
}
```

### PATCH:

#### request
```
{
    "userId": "0000-0000-000000",
    "userName": "alexander",
    "description": "description"
}
```

#### response 
```
{
    "userId": "0000-0000-000000",
    "userName": "alexander",
    "description": "description"
    "email": "alexnader@mail"
}
```

## Group

http://scheduler.com/group/{groupId}

### GET:

#### response
```
{
    "groupId": "0000-0000-000000",
    "groupName": "group1",
    "users": [
        {
            "userId": "0000-0000-000000",
            "userName": "alexander",
            "permissions": 123
        }
    ]
}
```

### PATCH:

#### request
```
{
    "groupName": "group1"
}
```

#### response
```
{
    "groupId": "0000-0000-000000",
    "groupName": "group1",
    "users": [
        {
            "userId": "0000-0000-000000",
            "userName": "alexander",
            "permissions": 123
        }
    ]
}
```

### DELETE:

#### response
```
{
    "groupId": "0000-0000-000000",
    "groupName": "group1",
    "users": [
        {
            "userId": "0000-0000-000000",
            "userName": "alexander",
            "permissions": 123
        }
    ]
}
```

### POST

#### request
```
{
    "groupName": "group1",
}
```

#### response
```
{
    "groupId": "0000-0000-000000",
    "groupName": "group1",
    "users": [
        {
            "userId": "0000-0000-000000",
            "userName": "alexander",
            "permissions": 123
        }
    ]
}
```

## Group user

http://scheduler.com/group/{groupId}/user/{userId}

### GET:

#### response
```
{
    "groupId": "0000-0000-000000",
    "userId": "0000-0000-000000",
    "permissions": "0000-0000-000000" // дописать тут чтобы таски его тут были
}
```

### PATCH:

#### request
```
{
    "groupId": "0000-0000-000000",
    "userId": "0000-0000-000000",
    "permissions": "0000-0000-000000"
}
```
#### response
```
{
    "groupId": "0000-0000-000000",
    "userId": "0000-0000-000000",
    "permissions": "0000-0000-000000" // дописать тут чтобы таски его тут были
}
```

### DELETE:

#### response
```
{
    "groupId": "0000-0000-000000",
    "userId": "0000-0000-000000",
    "permissions": "0000-0000-000000" // дописать тут чтобы таски его тут были
}
```

## Settings user

http://scheduler.com/user/{userId}/settings

### GET:

#### response
```
{
    "userId": "0000-0000-000000",
    "settings": 123
}
```

### PATCH:

#### request
```
{
    "settings": 123
}
```
#### response
```
{
    "userId": "0000-0000-000000",
    "settings": 123
}
```

## Invite user accept

http://scheduler.com/invite/user/{inviteId}

### GET:
```
{
    "inviteId": "0000-0000-000000",
    "senderId": "0000-0000-000000",
    "addressieId": "0000-0000-000000",
    "message": "message"
}
```

## Invite user create

http://scheduler.com/invite/user/{addressieId}

### POST:

Создает запрос в друзья, должна быть проверка, если по ссылке переходит автор,

#### request
```
{
    "message": "message"
}
```
#### response
```
{
    "inviteId": "0000-0000-000000",
    "senderId": "0000-0000-000000",
    "addressieId": "0000-0000-000000",
    "message": "message"
}
```

## Invite group accept

http://scheduler.com/group/invite/{groupId}?inviteId=0000-0000-000000

### POST:

#### response
```
{
    "groupId": "0000-0000-000000",
    "inviteId": "0000-0000-000000",
    "senderId": "0000-0000-000000",
    "permissions": 123,
    "message": "message"
}
```

## Invite group create

http://scheduler.com/group{groupId}/invite

### PUT:

#### request
```
{
    "uses": 1,
    "expire": "20.12.22",
    "permissions": 123,
    "message": "message"
}
```
#### response
```
{
    "inviteId": "0000-0000-000000",
    "senderId": "0000-0000-000000",
    "permissions": 123,
    "message": "message"
}
```

## Invite group delete

http://scheduler.com/group/{groupId}/invite/{inviteId}

### DELETE:

#### response
```
{
    "inviteId": "0000-0000-000000",
    "senderId": "0000-0000-000000",
    "permissions": 123,
    "message": "message"
}
```

## Tasks user

http://scheduler.com/tasks/user/

### GET:

#### response
```
{
    "count": 123,
    "userId": "0000-0000-0000000",
    "tasks": [
        {
            "taskId": "0000-0000-000000",
            "groupId": "0000-0000-000000", nullable
            "creatorId": "0000-0000-000000",
            "title": "title",
            "description": "description",
            "status": "new",
            "deadline": "20.12.22"
        }
    ]
}
```

## Tasks group

http://scheduler.com/tasks/group/{userId}

### GET:

#### response
```
{
    "count": 123,
    "groupId": "0000-0000-000000",
    "tasks": [
        {
            "taskId": "0000-0000-000000",
            "userId": "0000-0000-000000", nullable
            "creatorId": "0000-0000-000000",
            "title": "title",
            "description": "description",
            "status": "new",
            "deadline": "20.12.22"
        }
    ]
}
```

## Task

http://scheduler.com/task/{taskId?}

### GET:

#### response
```
{
    "taskId": "0000-0000-000000",
    "creatorId": "0000-0000-000000",
    "userId": "0000-0000-000000", nullable
    "groupId": "0000-0000-000000", nullable
    "title": "title",
    "description": "description",
    "status": 0,
    "deadline": "20.12.22"
}
```

PUT:

http://scheduler.com/task/

#### request
```
{
    "assignedUserId": "0000-0000-000000", nullable
    "groupId": "0000-0000-000000", nullable
    "title": "title",
    "description": "description",
    "status": 0,
    "deadline": "20.12.22"
}
```
#### response
```
{
    "taskId": "0000-0000-000000",
    "creatorId": "0000-0000-000000",
    "userId": "0000-0000-000000", nullable
    "groupId": "0000-0000-000000", nullable
    "title": "title",
    "description": "description",
    "status": 0,
    "deadline": "20.12.22"
}
```

### PATCH:

#### request
```
{
    "assignedUserId": "0000-0000-000000", nullable
    "groupId": "0000-0000-000000", nullable
    "title": "title",
    "description": "description",
    "status": 0,
    "deadline": "20.12.22"
}
```
#### response
```
{
    "taskId": "0000-0000-000000",
    "creatorId": "0000-0000-000000",
    "userId": "0000-0000-000000", nullable
    "groupId": "0000-0000-000000", nullable
    "title": "title",
    "description": "description",
    "status": 0,
    "deadline": "20.12.22"
}
```

### DELETE

#### response
```
{
    "taskId": "0000-0000-000000",
    "creatorId": "0000-0000-000000",
    "userId": "0000-0000-000000", nullable
    "groupId": "0000-0000-000000", nullable
    "title": "title",
    "description": "description",
    "status": 0,
    "deadline": "20.12.22"
}
```

## Finances



http://scheduler.com/finances

### GET
Returns user's private financialplans
#### response
```
{
    "count": 123,
    "plans": [
        {
            "financialId": "0000-0000-000000",
            "title": "title",
            "charges": [
                {
                    "id": "0000-0000-000000",
                    "chargeName": "charge name",
                    "description": "description",
                    "minimalCost": 123,
                    "maximalCost": 321,
                    "priority": 123,
                    "repeat": true,
                    "expire": datetimeoffset,
                    "created": "20.12.22"
                }
            ]
        }
    ]
}
```

http://scheduler.com/finances/{financialId}
### GET
Returns financial plan by id
#### response

```
{
    "financialId": "0000-0000-000000", 
    "title": "title",
    "charges": [
        {
            "id": "0000-0000-000000",
            "chargeName": "charge name",
            "description": "description",
            "minimalCost": 123,
            "maximalCost": 321,
            "priority": 123,
            "repeat": true,
            "expire": 31,
            "created": "20.12.22"
        }
    ]
}
```

http://scheduler.com/finances/group/{groupId}
### GET
Returns financial plans, attached to group
#### response

```
{
    "count": 123,
    "plans": [
        {
            "financialId": "0000-0000-000000",
            "title": "title",
            "charges": [
                {
                    "id": "0000-0000-000000",
                    "chargeName": "charge name",
                    "description": "description",
                    "minimalCost": 123,
                    "maximalCost": 321,
                    "priority": 123,
                    "repeat": true,
                    "expire": datetimeoffset,
                    "created": "20.12.22"
                }
            ]
        }
    ]
}
```

### PUT 
http://scheduler.com/finances

#### request
```
{
    "title": "title",
    "charges": [
        {
            "chargeName": "charge name",
            "description": "description",
            "minimalCost": 123,
            "maximalCost": 321,
            "priority": 123,
            "repeat": true,
            "expire": 31,
            "created": "20.12.22"
        }
    ]
}
```
#### response
```
{
    "financialId": "0000-0000-000000", 
    "title": "title",
    "charges": [
        {
            "id": "0000-0000-000000",
            "chargeName": "charge name",
            "description": "description",
            "minimalCost": 123,
            "maximalCost": 321,
            "priority": 123,
            "repeat": true,
            "expire": 31,
            "created": "20.12.22"
        }
    ]
}
```

### POST
http://scheduler.com/finances/{financialId}?title="title"
#### response
```
{
    "financialId": "0000-0000-000000", 
    "title": "title",
    "charges": [
        {
            "id": "0000-0000-000000",
            "chargeName": "charge name",
            "description": "description",
            "minimalCost": 123,
            "maximalCost": 321,
            "priority": 123,
            "repeat": true,
            "expire": 31,
            "created": "20.12.22"
        }
    ]
}
```

### DELETE
#### response
```
{
    "financialId": "0000-0000-000000", 
    "title": "title",
    "charges": [
        {
            "chargeName": "charge name",
            "description": "description",
            "minimalCost": 123,
            "maximalCost": 321,
            "priority": 123,
            "repeat": true,
            "expire": 31,
            "created": "20.12.22"
        }
    ]
}
```

http://scheduler.com/finances/{financialId}/calculate/fill?budget=123&priority=123
### GET
#### response
```
{
    "financialId": "0000-0000-000000",
    "title": "title",
    "limitDateOptimistic": "20.12.22",
    "limitDatePessimistic": "20.12.22",
    "charges": [
        {
            "id": "0000-0000-000000",
            "chargeName": "charge name",
            "description": "description",
            "minimalCost": 123,
            "maximalCost": 321,
            "priority": 123,
            "repeat": true,
            "expire": 31,
            "created": "20.12.22"
            "status": "enough|provided|notpaid"
        }
    ]
}
```

http://scheduler.com/finances/{financialId}/calculate/distribute?budget=123&date="yyyy-MM-ddTHH:mm:ssZZZ"
### GET
#### response
```
{
    "financialId": "0000-0000-000000",
    "title": "title",
    "charges": [
        {
            "id": "0000-0000-000000",
            "chargeName": "charge name",
            "description": "description",
            "minimalCost": 123,
            "maximalCost": 321,
            "priority": 123,
            "repeat": true,
            "expire": 31,
            "created": "20.12.22"
            "status": "enough|provided|notpaid"
        }
    ]
}
```

http://scheduler.com/finances/charges/{chargeId}
### GET
#### response
```
{
    "id": "0000-0000-000000",
    "chargeName": "charge name",
    "description": "description",
    "minimalCost": 123,
    "maximalCost": 321,
    "priority": 123,
    "repeat": true,
    "expire": 31,
    "created": "20.12.22"
}
```

http://scheduler.com/finances/{financialId}/charges
### PUT
#### request
```
{
    "chargeName": "charge name",
    "description": "description",
    "minimalCost": 123,
    "maximalCost": 321,
    "priority": 123,
    "repeat": true,
    "expire": 31,
}
```
#### response
```
{
    "id": "0000-0000-000000",
    "chargeName": "charge name",
    "description": "description",
    "minimalCost": 123,
    "maximalCost": 321,
    "priority": 123,
    "repeat": true,
    "expire": 31,
    "created": "20.12.22"
}
```


http://scheduler.com/finances/{financialId}/charges/{chargeId}
### POST 
#### request
```
{
    "chargeName": "charge name",
    "description": "description",
    "minimalCost": 123,
    "maximalCost": 321,
    "priority": 123,
    "repeat": true,
    "expire": 31,
}
```
#### response
```
{
    "id": "0000-0000-000000",
    "chargeName": "charge name",
    "description": "description",
    "minimalCost": 123,
    "maximalCost": 321,
    "priority": 123,
    "repeat": true,
    "expire": 31,
    "created": "20.12.22"
}
```

http://scheduler.com/finances/charges/{chargeId}
### DELETE 
#### response
```
{
    "id": "0000-0000-000000",
    "chargeName": "charge name",
    "description": "description",
    "minimalCost": 123,
    "maximalCost": 321,
    "priority": 123,
    "repeat": true,
    "expire": 31,
    "created": "20.12.22"
}
```

## Updates

http://scheduler.com/updates

#### request
```
[
    "updateType": "task.added",
    "data": {
        "taskId": "0000-0000-000000",
        "creatorId": "0000-0000-000000",
        "userId": "0000-0000-000000", nullable
        "groupId": "0000-0000-000000", nullable
        "title": "title",
        "description": "description",
        "status": "new",
        "deadline": "20.12.22"
    }
]
```

need to add copy as private for current user with checking for GroupId is null (financialPlan)