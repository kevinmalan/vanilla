## Vanilla (WIP)
The project showcases a collection of commonly used services.

### Auth [Auth](https://github.com/kevinmalan/vanilla/tree/master/Auth)
- Register and login.
- Password hashing.
- JWT generation.
- Cookie storage of JWT token.
- Unit tests.
- Data storage using PostgresSQL.

### Common [Common](https://github.com/kevinmalan/vanilla/tree/master/Common)
- Custom exception classes used in middleware exception handling.

### API [API](https://github.com/kevinmalan/vanilla/tree/master/API)
- HTTPS Endpoints exposing services to the end user.
- Registration of all dependencies.
- Handling exceptions.
- Hosted in a docker container [docker-compose.yml](https://github.com/kevinmalan/vanilla/blob/master/docker-compose.yml)


This project will evolve over time containing more services that makes use of the existing foundation (like auth, exception handling, logging, etc).