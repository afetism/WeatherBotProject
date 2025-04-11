🌦️ Telegram Hava Botu

Bu layihə Telegram bot vasitəsilə istifadəçilərə seçilmiş şəhər üzrə hava proqnozu təqdim edir. Layihə real iş müsahibəsi tapşırığına əsasən hazırlanmışdır və .NET ekosistemində API yaradılması, bot inteqrasiyası və verilənlər bazası ilə işləmə bacarıqlarını nümayiş etdirir.



📌 Funksionallıq

- `/weather {şəhər}` komandası ilə hava haqqında məlumat alın
- Telegram bot üzərindən şəhər seçimi üçün düymə (inline button)
- Hava məlumatı üçün OpenWeatherMap API inteqrasiyası
- Verilənlər bazasında istifadəçi və hava sorğusu tarixçəsinin saxlanılması
- İstifadəçi API-ləri:
  - `GET /users/{userId}` – İstifadəçi və onun hava tarixçəsi
  - `POST /sendWeatherToAll` – Seçilmiş hava məlumatını bütün istifadəçilərə göndərmək



⚙️ Texnologiyalar

- **.NET 8** – ASP.NET Core Web API
- **MS SQL** – Verilənlər bazası
- **Dapper** – Yüngül ORM
- **Telegram.Bot** – Telegram API ilə işləmək üçün NuGet paketi
- **OpenWeatherMap API** – Hava məlumatlarının alınması
- **Swagger** – API sənədləşdirməsi və test üçün

🗂️ Verilənlər Bazası Strukturu

- **Users** cədvəli:
  - `UserId` (int)
  - `TelegramId` (string)
  - `FirstName` (string)
  - `Username` (string)
  - `CreatedAt` (datetime)

- **WeatherHistory** cədvəli:
  - `Id` (int)
  - `UserId` (foreign key)
  - `City` (string)
  - `Temperature` (float)
  - `Description` (string)
  - `QueriedAt` (datetime)
1. Repozitoriyanı klonla

```bash
git clone https://github.com/afetism/WeatherBotProject.git
cd WeatherBotProject



