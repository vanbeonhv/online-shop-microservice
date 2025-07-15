### Architecture Overview

![Architecture Overview](assets/architecture.png)

### Docker Compose Setup for Local Development
```bash
docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml up -d --remove-orphans
```

Linux
```bash
 docker compose -f docker-compose.yml -f docker-compose.override.yml up -d --remove-orphans
```
