# Swift799_API
This README provides an overview of the Swift799 API.
It includes information about the `add message` endpoint that allows users to submit SWIFT MT 799 messages via a POST request.
After a message has been submited the main purpose of the application is to break down the free form text in the message and add it to the included SQLite local database.

## API Endpoint

### Add Message

Use this endpoint to add a new message.

-   Endpoint: `/api/messages`
-   Method: `POST`
-   Consumes: `text/plain`


## SQLite database

### Table Messages example:

|  message_id |  transaction_reference_number |  related_reference |  narrative |
|------------ |-------------------------------|--------------------|------------|
|        1    |       67-C111111-KNTRL        |   30-111-1111111   |  SOME TEXT |

-   `message_id`: INT, PK, AUTO INCREMENT
-   `transaction_reference_number`: VARCHAR2(50) NOT NULL
-   `related_reference`: VARCHAR2(50)
-   `narrative`: TEXT NOT NULL

## Logging
`Serilog` has been used for the logging of data.
It logs both to the console and in seprate files located in the Swift799_API\Files\logs folder.

##License
This project is licensed under the MIT License.
