# As a user I want to login to the application

## Fields

| Field           | Control (format)  | Required | Type   | Description              | Length | Min | Max | Precision | Scale |
| --------------- | ----------------- | -------- | ------ | ------------------------ | ------ | --- | --- | --------- | ----- |
| Email           | Textbox (email)   | Yes      | String | User's email address     | 50     |     |     |           |       |
| Password        | Password          | Yes      | String | Account password         | 50     |     |     |           |       |
| Email Token     | Button            |          |        | Sends Tfa Token          |        |     |     |           |       |
| Sms Token       | Button            |          |        | Sends Tfa Token          |        |     |     |           |       |
| Token           | Textbox(6 digits) | Yes      | Number | Account password         | 50     |     |     |           |       |
| Login           | Button            |          |        | Login to the application |        |     |     |           |       |
| Forgot Password | Link              |          |        | Login to the application |        |     |     |           |       |

## "Email Token" and "Sms Token" is enabled only if "Email" and "Password" has valid values

## "Login" is enabled only if "Email", "Password", and "Token" has valid values
