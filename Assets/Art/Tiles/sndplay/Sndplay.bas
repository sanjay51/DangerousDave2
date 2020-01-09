CLS
IF COMMAND$ = "" THEN
  PRINT "PC Speaker Sound Player by Frenkel Smeijers, March 23 2002."
  PRINT "Usage: sndplay [sound file]"
  PRINT "Example: sndplay sounds.ck1"
  END
END IF

OPEN COMMAND$ FOR BINARY AS #1
IF LOF(1) = 0 THEN
  PRINT "Can't find " + COMMAND$
  CLOSE #1
  KILL COMMAND$
  END
END IF

DIM signature AS STRING * 3
GET #1, , signature
IF signature <> "SND" THEN
  IF signature <> "SPK" THEN
    PRINT "Bad file"
    CLOSE #1
    END
  END IF
END IF

DIM nrofsounds AS INTEGER
DIM offset AS INTEGER
DIM priority AS STRING * 1
DIM exist AS STRING * 1
DIM filename AS STRING * 12
DIM toon AS INTEGER
DIM x AS INTEGER
DIM duration AS SINGLE
x = 1
duration = 1 / 128 * 18.2
GET #1, 7, nrofsounds
IF signature = "SPK" THEN nrofsounds = 63
soundnumber = 1

getdata:
GET #1, soundnumber * 16 + 1, offset
GET #1, , priority
GET #1, , exist
GET #1, , filename
CLS
PRINT " nr filename     priority"
IF soundnumber < 10 THEN LOCATE 2, 2
PRINT soundnumber; filename; ASC(priority)

playsound:
GET #1, offset + x, toon
IF EOF(1) <> 0 THEN GOTO toets
IF toon = &HFFFF THEN
  x = 1
  GOTO toets
ELSEIF toon >= 37 AND toon <= 32767 THEN
  SOUND 1193180 / toon, duration
ELSE
  SOUND 0, duration
END IF
x = x + 2
GOTO playsound

toets:
a$ = INKEY$
IF a$ = CHR$(0) + "M" AND soundnumber < nrofsounds THEN
  soundnumber = soundnumber + 1
  GOTO getdata
ELSEIF a$ = CHR$(0) + "K" AND soundnumber > 1 THEN
  soundnumber = soundnumber - 1
  GOTO getdata
ELSEIF a$ = " " THEN GOTO playsound
ELSEIF a$ = CHR$(27) THEN
  CLOSE #1
  END
END IF
GOTO toets

