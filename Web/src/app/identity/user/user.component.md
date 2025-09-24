# As am admin I want to add/edit/delete users for the application

## Fields in a Table

| Field    | Control (format) | Required | Type   | Description           | Length | Min | Max | Precision | Scale |
| -------- | ---------------- | -------- | ------ | --------------------- | ------ | --- | --- | --------- | ----- |
| Email    | Textbox (email)  | Yes      | String | User's email address  | 50     |     |     |           |       |
| Phone    | Password         | Yes      | String | Account password      | 50     |     |     |           |       |
| IsActive | Checkbox         | Yes      |        | Is the Account Active |        |     |     |           |       |
| UnLock   | Button           |          |        | Unlock the account    |        |     |     |           |       |
|          |                  |          |        |                       |        |     |     |           |       |
|          |                  |          |        |                       |        |     |     |           |       |

# "UnLock" will be visible if the account is locked
