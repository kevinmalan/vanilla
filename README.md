## Vanilla (WIP)
The project showcases a collection of commonly used services.

### Auth (https://github.com/kevinmalan/vanilla/tree/master/API)
- Register and login.
- Password hashing.
- JWT generation.
- Cookie storage of JWT token.
- Unit tests.
- Data storage using PostgresSQL.

### Common (https://github.com/kevinmalan/vanilla/tree/master/Common)
- Custom exception classes used in middleware exception handling.

### API (https://github.com/kevinmalan/vanilla/tree/master/API)
- Endpoints exposing all the services to the end user.
- Registration of all dependencies.
- Handling exceptions.
- Hosted in a docker container (https://github.com/kevinmalan/vanilla/blob/master/docker-compose.yml).


This project will evolve over time containing more services that makes use of the existing foundation (like auth, exception handling, logging, etc).