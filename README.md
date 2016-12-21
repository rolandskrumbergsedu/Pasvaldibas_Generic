# Pasvaldibas_Generic

### In order to handle new municipality follow these steps:
1. Create new handler class in folder Handlers in PdfParsing.Logic project
2. This class should inherit GenericHandler
3. Copy implementation from other existing handler (change of course constructor name)
4. Update following proporties:
  * AttendedStartIndexMark 
  * AttendedEndIndexMark
  * NotAttendedStartIndexMark
  * NotAttendedEndIndexMark
  * AttendedSplitOptions
  * NotAttendedSplitOptions
  * NotAttendedInternalSplitOptions
  * Deputati
  * Name
  * Prieksedetajs
  * DeputatuSkaits
  * AttendedSplit
  * NotAttendedSplit
  * AttendedNextLine
  * NotAttendedNextLine
5. Update these methods after first batch is run
  * ReplaceAttended
  * ReplaceNotAttended
6. Copy new unit test and update handler name there
