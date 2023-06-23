### Time

I got carried away and lost track of time. I took 2hrs 45 mins to get this working.

#### Changes

- Organised project structure.
- Introduced an IDataStore contract in Data for reusability/testability and also reduced code duplication.
- Added MailChecker to separate logic from the MainTransferService and a contract for testing the logic.
- Added simple test calling each mail types.


#### Improvement

- Add serilog for debugging
- Create exception handler with a clear message of what went wrong.
- write more tests to cover other happy paths and handle exception
- 