# Backend unit tesztek – dokumentáció (CastL / Main API)

> A képeket (Visual Studio Test Explorer, `dotnet test` kimenet) te illeszd be a Tesztdokumentáció Word dokumentumba.

## 1. Bevezetés

A frontendhez hasonlóan a **Main API** (`BackEnd`) részére **xUnit** alapú **unit tesztek** készültek. A cél nem a teljes API lefedése, hanem néhány, jól elkülöníthető, **tesztelhető komponens** ellenőrzése: a **profilkép URL összeállítása** (`ProfilePictureHelper`) és az **Auth API HTTP kliens** bemeneti ellenőrzése illetve sikeres hívásának viselkedése (`AuthApiClient`).

A tesztek a **`BackEnd.Tests`** projektben találhatók, futtatás: a `BackEnd` mappában  
`dotnet test BackEnd.Tests/BackEnd.Tests.csproj`

## 2. Tesztelt funkciók kiválasztása

| Komponens | Miért ideális unit teszthez |
|-----------|-----------------------------|
| **ProfilePictureHelper** | Egyértelmű ágak: nincs user, van egyedi kép fájlnév, van alapértelmezett kép táblából. EF Core **InMemory** adatbázissal gyorsan szimulálható. |
| **AuthApiClient** | **HttpClient** mockolható saját `HttpMessageHandler`-rel; a **konfiguráció** JSON-ból memóriában; jól tesztelhető a **hibás bemenet** és a **DELETE** kérés + fejléc. |

## 3. Tesztesetek (összefoglaló – 4 unit teszt)

### TC-BE-01 – Profilkép: egyedi feltöltött fájl

- **Cél:** Ha a felhasználónak van **`CustomPictureUrl`** (fájlnév), a visszaadott cím legyen a fix bázis URL + fájlnév (éles környezetben: `https://dongesz.com/images/...`).
- **Elvárás:** A visszaadott string pontosan egyezik az elvárt összefűzött URL-lel.

### TC-BE-02 – Profilkép: alapértelmezett kép a `DefaultPictures` táblából

- **Cél:** Ha nincs egyedi kép, de van **`DefaultPictureUrl`** foreign key és a táblában szerepel a teljes URL, az kerüljön vissza.
- **Elvárás:** A helper a **`DefaultPictures.DefaultPictureUrl`** értékét adja vissza (nem a fájlnév + bázis URL mintát).

### TC-BE-03 – Auth kliens: üres `authUserId`

- **Cél:** A **`DeleteIdentityUserAsync`** ne induljon el érvénytelen azonosítóval.
- **Elvárás:** **`ArgumentException`** dobódik (pl. csak whitespace bemenet).

### TC-BE-04 – Auth kliens: sikeres törlés és HTTP részletek

- **Cél:** Konfigurált **BaseUrl** és **InternalApiKey** mellett a kliens **DELETE** kérést küld, **`X-Internal-Api-Key`** fejléccel; sikeres válasz esetén **`true`** a visszatérés.
- **Elvárás:** HTTP státusz sikeres (pl. 204), metódus **DELETE**, az URL tartalmazza az `authUserId`-t, a fejléc tartalmazza a kulcsot; a metódus **`true`**.

## 4. Technikai megjegyzések (dokumentációhoz / védéshez)

- A **`BackEnd.csproj`** kizárja a **`BackEnd.Tests`** almappát a fordításból (`DefaultItemExcludes`), hogy a tesztprojekt fájljai ne kerüljenek véletlenül a webalkalmazás DLL-jébe.
- **EF Core InMemory** nem teljes értékű MySQL helyettesítő (pl. triggerek, collation), de ezekhez a tesztekhez elég a modell és a lekérdezések viselkedésének igazolására.

---

*Generált szöveg a CastL vizsga / tesztdokumentáció kiegészítéséhez.*
