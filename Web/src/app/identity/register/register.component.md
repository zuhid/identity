# As a user I want to create an account so that I can access the application

## Fields

| Field             | Control (format) | Required | Type   | Description                                      | Length | Min | Max | Precision | Scale |
| ----------------- | ---------------- | -------- | ------ | ------------------------------------------------ | ------ | --- | --- | --------- | ----- |
| Email             | Textbox (email)  | Yes      | String | User's email address                             | 50     |     |     |           |       |
| Password          | Password         | Yes      | String | Account password                                 | 50     |     |     |           |       |
| Re-Enter Password | Password         | Yes      | String | Reenter the password to make sure it is the same | 50     |     |     |           |       |
| Phone             | Textbox (phone)  | No       | String | User's phone number                              | 50     |     |     |           |       |
| Register          | Button           |          |        | Creates an account                               |        |     |     |           |       |
| Go to Login       | Link             |          |        | Take the user to the login page                  |        |     |     |           |       |

## When I create an account, an email is sent to me to verify my email

## If an email is already registered, notify the users
