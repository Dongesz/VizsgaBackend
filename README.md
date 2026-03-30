# Vizsga Backend
## Oldal felépítése
```text
VizsgaBackend/
├── BackEnd/                    # Main backend API (játék logika, users, scoreboard, admin)
│   ├── Api/Controllers/        # HTTP végpontok (Admin, Users, Scoreboard, stb.)
│   ├── Application/            # DTO-k, service-ek, helper-ek, mapper-ek
│   ├── Domain/                 # Entitások / modellek
│   ├── Infrastructure/         # DbContext és adatbázis réteg
│   ├── Migrations/             # EF Core migrációk
│   ├── BackEnd.csproj
│   └── BackEnd.sln
│
└── AuthApi/RoleBasedAuth/      # Auth API (register/login/role kezelés/JWT)
    ├── Api/Controllers/        # Auth végpontok
    ├── Application/            # DTO-k, auth service-ek
    ├── Domain/                 # Identity modellek
    ├── Data/                   # Auth DbContext
    ├── Migrations/             # EF migrációk
    └── RoleBasedAuth.csproj
```
## Futtatás (.NET)
1. Előfeltétel:
   - .NET SDK 8 telepítve
2. Main backend indítása:
   - `cd BackEnd`
   - `dotnet restore`
   - `dotnet run`
3. Auth API indítása (új terminálban):
   - `cd AuthApi/RoleBasedAuth`
   - `dotnet restore`
   - `dotnet run`
4. Swagger (lokál):
   - Main backend: [https://localhost:7282/api/main/swagger](https://localhost:7282/api/main/swagger)
   - Auth API: [https://localhost:7224/api/auth/swagger](https://localhost:7224/api/auth/swagger)
A fenti futtatási parancsok után a Swagger felületek online linkeken is elérhetőek a telepített (deployed) környezetben.
