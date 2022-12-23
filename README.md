<h1> Sprendžiamo uždavinio aprašymas </h1>
<h2> Sistemos paskirtis </h2>

Projekto tikslas – sukurti sistemą, kuri leistų naudotojams kurti nuomojamų patalpų skelbimus bei juos rezervuoti.

Svečiai turės galimybę peržiūrėti miestus ir juose esančius skelbimus bei jų informaciją. Registruoti naudotojai galės sukurti skelbimą, atlikti rezervacijas ir juos redaguoti arba pašalinti. Administratoriai turės visus sistemos teikiamus funkcionalumus.

<h2> Funkciniai reikalavimai </h2>
Svečio funkcijos:

-	Peržiūrėti miestų sąrašą
-	Peržiūrėti skelbimų sąrašą
-	Peržiūrėti skelbimo informaciją
- Peržiūrėti rezervacijas
-	Registruotis

Registruoto naudotojo funkcijos:

-	Peržiūrėti miestų sąrašą
-	Peržiūrėti / sukurti / redaguoti / šalinti savo skelbimą
-	Peržiūrėti / sukurti / redaguoti / šalinti savo rezervaciją
-	Prisijungti
-	Atsijungti

Administratoriaus funkcijos:

-	Peržiūrėti / sukurti / redaguoti / šalinti bet kurį miestą
-	Peržiūrėti / sukurti / redaguoti / šalinti bet kurį skelbimą
-	Peržiūrėti / sukurti / redaguoti / šalinti bet kurią rezervaciją
-	Prisijungti
-	Atsijungti

<h1> Sistemos architektūra </h1>

Sistemos sudedamosios dalys:

-	Kliento pusė (front-end) – realizuojama naudojant React.js;
-	Serverio pusė (back-end) – realizuojama naudojant .NET Core 6. Duomenų bazė - MSSQL

UML Deployment diagrama:

![Screenshot_1](https://user-images.githubusercontent.com/79587813/209364599-17b3e381-873a-4b14-a812-93e6a311b0ec.png)


<h1> Naudotojo sąsajos projektas </h1>

<h2> Prisijungimo langas </h2>

<h3> Wireframe </h3>

![Screenshot_4](https://user-images.githubusercontent.com/79587813/209365867-2f00dde1-30c8-409b-8eeb-be9297ecb157.png)

<h3> Realizacijos iškarpa </h3>

![Screenshot_1](https://user-images.githubusercontent.com/79587813/209364902-9faf86ac-1dc2-41c9-adf9-10c802f2f831.png)

<h2> Registracijos langas </h2>

<h3> Wireframe </h3>

![Screenshot_5](https://user-images.githubusercontent.com/79587813/209366109-980f7a95-0c84-4ec6-b929-64462400a5fc.png)

<h3> Realizacijos iškarpa </h3>

![Screenshot_2](https://user-images.githubusercontent.com/79587813/209365208-37b61b4c-8666-4c93-9605-e13f77604352.png)

<h2> Skelbimų sąrašo langas </h2>

<h3> Wireframe </h3>

![Screenshot_6](https://user-images.githubusercontent.com/79587813/209367958-ac6bb2e5-e4de-4864-958b-b0b6307856cc.png)

<h3> Realizacijos iškarpa </h3>

![Screenshot_3](https://user-images.githubusercontent.com/79587813/209365338-da52caef-e0a4-4de3-8108-b1f97da4d9d0.png)

<h1> API specifikacija </h1>

<h2> Miestų API metodai </h2>

<h3> GET /cities </h3>

<h4> Metodo URL </h4>
/api/cities
<h4> Informacija apie metodą </h4>

Atsakymo formatas | JSON
--- | ---
__Reikalaujama autentifikacija?__ | __Ne__
__Atsakymo kodai__ | __200, 404__

<h4> Užklausos pavyzdys </h4>

```
GET /api/cities
```

<h4> Atsakymo pavyzdys </h4>

```json
[
    {
        "id": 1,
        "name": "Kaunas",
        "description": "test"
    },
    {
        "id": 3,
        "name": "Vilnius",
        "description": "editdesc123123"
    }
]
```

<h3> GET /cities/{cityId} </h3>

<h4> Metodo URL </h4>
/api/cities/{cityId}
<h4> Informacija apie metodą </h4>

Atsakymo formatas | JSON
--- | ---
__Reikalaujama autentifikacija?__ | __Ne__
__Atsakymo kodai__ | __200, 404__

<h4> Užklausos pavyzdys </h4>

```
GET /api/cities/1
```

<h4> Atsakymo pavyzdys </h4>

```json
{
    "id": 1,
    "name": "Kaunas",
    "description": "test"
}
```

<h3> POST /cities </h3>

<h4> Metodo URL </h4>
/api/cities/{cityId}
<h4> Informacija apie metodą </h4>

Atsakymo formatas | JSON
--- | ---
__Reikalaujama autentifikacija?__ | __Taip__
__Atsakymo kodai__ | __201, 400, 401, 403, 404__

<h4> Parametrai </h4>

Pavadinimas  | Aprašymas | Pavyzdys
--- | --- | ---
Name | Miesto pavadinimas | Kaunas
Description | Miesto aprašymas | test

<h4> Užklausos pavyzdys </h4>

```
POST /api/cities
```

<h4> Atsakymo pavyzdys </h4>

```json
{
    "id": 1,
    "name": "Kaunas",
    "description": "test"
}
```

<h3> PUT /cities/{cityId} </h3>

<h4> Metodo URL </h4>
/api/cities/{cityId}
<h4> Informacija apie metodą </h4>

Atsakymo formatas | JSON
--- | ---
__Reikalaujama autentifikacija?__ | __Taip__
__Atsakymo kodai__ | __200, 400, 401, 403, 404__

<h4> Parametrai </h4>

Pavadinimas  | Aprašymas | Pavyzdys
--- | --- | ---
Description | Miesto aprašymas | test2

<h4> Užklausos pavyzdys </h4>

```
PUT /api/cities/1
```

<h4> Atsakymo pavyzdys </h4>

```json
{
    "id": 1,
    "name": "Kaunas",
    "description": "test2"
}
```

<h3> DEL /cities/{cityId} </h3>

<h4> Metodo URL </h4>
/api/cities/{cityId}
<h4> Informacija apie metodą </h4>

Atsakymo formatas | JSON
--- | ---
__Reikalaujama autentifikacija?__ | __Taip__
__Atsakymo kodai__ | __204, 400, 401, 403, 404__

<h4> Užklausos pavyzdys </h4>

```
DEL /api/cities/1
```

<h4> Atsakymo pavyzdys </h4>

```json
No Content
```

<h3> GET /cities/{cityId}/cityreservations </h3>

<h4> Metodo URL </h4>
/api/cities/{cityId}/cityreservations
<h4> Informacija apie metodą </h4>

Atsakymo formatas | JSON
--- | ---
__Reikalaujama autentifikacija?__ | __Ne__
__Atsakymo kodai__ | __200, 404__

<h4> Užklausos pavyzdys </h4>

```
GET /api/cities/1/cityreservations
```

<h4> Atsakymo pavyzdys </h4>

```json
[
    {
        "id": 1,
        "guestCount": 1,
        "startDate": "2022-12-01T00:00:00",
        "endDate": "2022-12-02T00:00:00"
    },
    {
        "id": 2,
        "guestCount": 2,
        "startDate": "2022-12-03T00:00:00",
        "endDate": "2022-12-04T00:00:00"
    }
]
```

<h2> Skelbimų API metodai </h2>

<h3> GET /cities/{cityId}/ads </h3>

<h4> Metodo URL </h4>
/api/cities/{cityId}/ads
<h4> Informacija apie metodą </h4>

Atsakymo formatas | JSON
--- | ---
__Reikalaujama autentifikacija?__ | __Ne__
__Atsakymo kodai__ | __200, 404__

<h4> Užklausos pavyzdys </h4>

```
GET /api/cities/1/ads
```

<h4> Atsakymo pavyzdys </h4>

```json
[
    {
        "id": 2,
        "price": 123,
        "bedCount": 3
    },
    {
        "id": 3,
        "price": 777,
        "bedCount": 3
    }
]
```

<h3> GET /cities/{cityId}/ads/{adId} </h3>

<h4> Metodo URL </h4>
/api/cities/{cityId}/ads/{adId}
<h4> Informacija apie metodą </h4>

Atsakymo formatas | JSON
--- | ---
__Reikalaujama autentifikacija?__ | __Ne__
__Atsakymo kodai__ | __200, 404__

<h4> Užklausos pavyzdys </h4>

```
GET /api/cities/1/ads/2
```

<h4> Atsakymo pavyzdys </h4>

```json
{
    "id": 2,
    "price": 123,
    "bedCount": 3
}
```

<h3> POST /cities/{cityId}/ads </h3>

<h4> Metodo URL </h4>
/api/cities/{cityId}/ads
<h4> Informacija apie metodą </h4>

Atsakymo formatas | JSON
--- | ---
__Reikalaujama autentifikacija?__ | __Taip__
__Atsakymo kodai__ | __201, 400, 401, 403, 404__

<h4> Parametrai </h4>

Pavadinimas  | Aprašymas | Pavyzdys
--- | --- | ---
Price | Skelbimo kaina | 123
BedCount | Lovų skaičius nuomojamoje patalpoje | 3

<h4> Užklausos pavyzdys </h4>

```
POST /api/cities/1/ads
```

<h4> Atsakymo pavyzdys </h4>

```json
{
    "id": 2,
    "price": 123,
    "bedCount": 3
}
```

<h3> PUT /cities/{cityId}/ads/{adId} </h3>

<h4> Metodo URL </h4>
/api/cities/{cityId}/ads/{adId}
<h4> Informacija apie metodą </h4>

Atsakymo formatas | JSON
--- | ---
__Reikalaujama autentifikacija?__ | __Taip__
__Atsakymo kodai__ | __200, 400, 401, 403, 404__

<h4> Parametrai </h4>

Pavadinimas  | Aprašymas | Pavyzdys
--- | --- | ---
Price | Skelbimo kaina | 345

<h4> Užklausos pavyzdys </h4>

```
PUT /api/cities/1/ads/2
```

<h4> Atsakymo pavyzdys </h4>

```json
{
    "id": 2,
    "price": 345,
    "bedCount": 3
}
```

<h3> DEL /cities/{cityId}/ads/{adId} </h3>

<h4> Metodo URL </h4>
/api/cities/{cityId}/ads/{adId}
<h4> Informacija apie metodą </h4>

Atsakymo formatas | JSON
--- | ---
__Reikalaujama autentifikacija?__ | __Taip__
__Atsakymo kodai__ | __204, 400, 401, 403, 404__

<h4> Užklausos pavyzdys </h4>

```
DEL /api/cities/1/ads/2
```

<h4> Atsakymo pavyzdys </h4>

```json
No Content
```

<h2> Rezervacijų API metodai </h2>

<h3> GET /cities/{cityId}/ads/{adId}/reservations </h3>

<h4> Metodo URL </h4>
/api/cities/{cityId}/ads/{adId}/reservations
<h4> Informacija apie metodą </h4>

Atsakymo formatas | JSON
--- | ---
__Reikalaujama autentifikacija?__ | __Ne__
__Atsakymo kodai__ | __200, 404__

<h4> Užklausos pavyzdys </h4>

```
GET /api/cities/1/ads/2/reservations
```

<h4> Atsakymo pavyzdys </h4>

```json
[
    {
        "id": 1,
        "guestCount": 1,
        "startDate": "2022-12-01T00:00:00",
        "endDate": "2022-12-02T00:00:00"
    },
    {
        "id": 2,
        "guestCount": 2,
        "startDate": "2022-12-03T00:00:00",
        "endDate": "2022-12-04T00:00:00"
    }
]
```

<h3> GET /cities/{cityId}/ads/{adId}/reservations/{reservationId} </h3>

<h4> Metodo URL </h4>
/api/cities/{cityId}/ads/{adId}/reservations/{reservationId}
<h4> Informacija apie metodą </h4>

Atsakymo formatas | JSON
--- | ---
__Reikalaujama autentifikacija?__ | __Ne__
__Atsakymo kodai__ | __200, 404__

<h4> Užklausos pavyzdys </h4>

```
GET /api/cities/1/ads/2/reservations/1
```

<h4> Atsakymo pavyzdys </h4>

```json
{
    "id": 1,
    "guestCount": 1,
    "startDate": "2022-12-01T00:00:00",
    "endDate": "2022-12-02T00:00:00"
}
```

<h3> POST /cities/{cityId}/ads/{adId}/reservations </h3>

<h4> Metodo URL </h4>
/api/cities/{cityId}/ads/{adId}/reservations
<h4> Informacija apie metodą </h4>

Atsakymo formatas | JSON
--- | ---
__Reikalaujama autentifikacija?__ | __Taip__
__Atsakymo kodai__ | __201, 400, 401, 403, 404__

<h4> Parametrai </h4>

Pavadinimas  | Aprašymas | Pavyzdys
--- | --- | ---
GuestCount | Žmonių kiekis gyvenančių patalpoje nuomos metu | 1
StartDate | Rezervacijos pradžia | 2022-12-01
EndDate | Rezervacijos pabaiga | 2022-12-02

<h4> Užklausos pavyzdys </h4>

```
POST /api/cities/1/ads/2/reservations
```

<h4> Atsakymo pavyzdys </h4>

```json
{
    "id": 1,
    "guestCount": 1,
    "startDate": "2022-12-01T00:00:00",
    "endDate": "2022-12-02T00:00:00"
}
```

<h3> PUT /cities/{cityId}/ads/{adId}/reservations/{reservationId} </h3>

<h4> Metodo URL </h4>
/api/cities/{cityId}/ads/{adId}/reservations
<h4> Informacija apie metodą </h4>

Atsakymo formatas | JSON
--- | ---
__Reikalaujama autentifikacija?__ | __Taip__
__Atsakymo kodai__ | __200, 400, 401, 403, 404__

<h4> Parametrai </h4>

Pavadinimas  | Aprašymas | Pavyzdys
--- | --- | ---
StartDate | Rezervacijos pradžia | 2022-12-01
EndDate | Rezervacijos pabaiga | 2022-12-03

<h4> Užklausos pavyzdys </h4>

```
PUT /api/cities/1/ads/2/reservations/1
```

<h4> Atsakymo pavyzdys </h4>

```json
{
    "id": 1,
    "guestCount": 1,
    "startDate": "2022-12-01T00:00:00",
    "endDate": "2022-12-03T00:00:00"
}
```

<h3> DEL /cities/{cityId}/ads/{adId}/reservations/{reservationId} </h3>

<h4> Metodo URL </h4>
/api/cities/{cityId}/ads/{adId}/reservations
<h4> Informacija apie metodą </h4>

Atsakymo formatas | JSON
--- | ---
__Reikalaujama autentifikacija?__ | __Taip__
__Atsakymo kodai__ | __204, 400, 401, 403, 404__

<h4> Užklausos pavyzdys </h4>

```
DEL /api/cities/1/ads/2/reservations/1
```

<h4> Atsakymo pavyzdys </h4>

```json
No Content
```

<h2> Prisijungimo / Registracijos API metodai </h2>

<h3> POST /register </h3>

<h4> Metodo URL </h4>
/api/register
<h4> Informacija apie metodą </h4>

Atsakymo formatas | JSON
--- | ---
__Reikalaujama autentifikacija?__ | __Ne__
__Atsakymo kodai__ | __201, 400, 404__

<h4> Parametrai </h4>

Pavadinimas  | Aprašymas | Pavyzdys
--- | --- | ---
Username | Naudotojo vardas | user5
Email | Naudotojo elektroninio pašto adresas | user5@u.com
Password | Naudotojo slaptažodis | Asdasd!1

<h4> Užklausos pavyzdys </h4>

```
POST /api/register
```

<h4> Atsakymo pavyzdys </h4>

```json
{
    "id": "9ac55abc-8372-4724-ab0e-4554370a02f6",
    "userName": "user5",
    "email": "user5@u.com"
}
```

<h3> POST /login </h3>

<h4> Metodo URL </h4>
/api/login
<h4> Informacija apie metodą </h4>

Atsakymo formatas | JSON
--- | ---
__Reikalaujama autentifikacija?__ | __Ne__
__Atsakymo kodai__ | __200, 400, 404__

<h4> Parametrai </h4>

Pavadinimas  | Aprašymas | Pavyzdys
--- | --- | ---
Username | Naudotojo vardas | user5
Password | Naudotojo slaptažodis | Asdasd!1

<h4> Užklausos pavyzdys </h4>

```
POST /api/login
```

<h4> Atsakymo pavyzdys </h4>

```json
{
    "accessToken": "JWT token"
}
```

<h1> Išvados </h1>

Kuriant projektą buvo įgyta žinių apie RESTful API ir React karkasą, bei išmokta sukurti nedidelę programą jais pasinaudojant. Taip pat pavyko įgilinti žinias apie projekto talpinimą pasitelkiant debesų kompiuteriją.
