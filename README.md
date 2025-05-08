# User Registration Wizard

Веб-приложение для регистрации пользователей с использованием ASP.NET Core (Web API) и Angular. Реализована пошаговая форма регистрации с динамической загрузкой провинций по выбранной стране, валидацией и обработкой ошибок.

---

## Технологии

- **Frontend:** Angular 17, Angular Material, Reactive Forms
- **Backend:** ASP.NET Core Web API (.NET 8)
- **Database:** PostgreSQL

---

## Функциональность

- **Step 1**: Ввод email, пароля, подтверждение пароля, согласие с условиями.
- **Step 2**: Выбор страны и провинции (провинции загружаются через AJAX).
- Валидация всех полей.
- Ошибки с бэкенда отображаются через `MatSnackBar`.
- Регистрация сохраняется в базу данных.

---

## Как запустить
1. Открыть решение в Visual Studio
2. Открыть appsetting.json в проекте WebApi и проверить ConnectionStrings к базе данных PostgreSQL, подправить соответственно. 
3. Нажать F5 — проект запустится по адресу:
### Angular
http://localhost:56027/
### WebApi
https://localhost:7222/swagger/index.html


---

## API Endpoints
- POST /Auth/register — регистрация пользователя

- GET /Country — список стран

- GET /Province?countryId=X — провинции выбранной страны

---

## Пример запроса
```
POST /Auth/register
{
  "email": "user@example.com",
  "password": "P@ssw0rd1",
  "confirmPassword": "P@ssw0rd1",
  "agreed": true,
  "provinceId": 3
}
```