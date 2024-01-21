Imports System.IO

Module App
    'These variables have been set a global so they don't have to be repeated in various subs
    Dim MenuChoice, UsernameCheck, path, path2, Line, StudentUsername, TeacherUsername, Test As String
    Dim LoginFound As Boolean
    Structure TAddWordandDefinition
        'This is a record of the words and definitions entered by the teacher
        Dim Word, Definition As String
    End Structure
    'Sub where the user chooses to either login as student, teacher or quit the system
    Sub Main()
        'Changes the colour of the background and foreground of the console
        Console.BackgroundColor = ConsoleColor.White
        Console.ForegroundColor = ConsoleColor.Black

        'The student and teacher username have be set to nothing, this is because they are used to determine who is logged in in later subroutines.
        StudentUsername = ""
        TeacherUsername = ""

        'At the beginning of each user interface I clear the screen so it is clearer and is nicer to look at when switching between screens and menus.
        Console.Clear()
        Console.WriteLine("")
        Console.WriteLine("")
        Console.WriteLine("")
        Console.WriteLine("")
        Console.WriteLine("")
        Console.WriteLine("")
        Console.WriteLine("")
        Console.WriteLine("                     ╔════════════════════════════╗")
        Console.WriteLine("                     ║Welcome to the Spelling Bee!║")
        Console.WriteLine("                     ╚════════════════════════════╝")
        Console.WriteLine("")
        Console.WriteLine("                      Press any key to continue.")

        Console.ReadLine()

        Console.Clear()
        Console.WriteLine("                             Main Menu")
        Console.WriteLine("                     Welcome to the Spelling Bee!")
        Console.WriteLine("                     ============================")
        Console.WriteLine("")
        Console.WriteLine("             Enter the number of the option you want to pick:")
        Console.WriteLine("")
        Console.WriteLine("                         1. Login as Student")
        Console.WriteLine("                         2. Login as Teacher")
        Console.WriteLine("                         3. Quit")

        'This loop allows the user to choose whether they would like to login as student, teacher or quit. The user will then be redirected to the correct menu
        MenuChoice = Console.ReadLine
        Do
            Select Case MenuChoice
                Case "1"
                    StudentLogin()
                Case "2"
                    TeacherLogin()
                Case "3"
                    QuittingMenu()
                Case Else
                    Console.WriteLine("Enter a number between 1 and 3 to select your option")
                    MenuChoice = Console.ReadLine
                    Select Case MenuChoice
                        Case "1"
                            StudentLogin()
                        Case "2"
                            TeacherLogin()
                        Case "3"
                            QuittingMenu()
                    End Select
            End Select
        Loop
        Console.Read()
    End Sub
    'Sub where the student enters their username and password to login
    Sub StudentLogin()
        'Initialise variables
        Dim StudentPassword As String
        'The path of the file that the user wants to access
        path = Directory.GetCurrentDirectory() & "\studentlogindetails.txt"
        'Login is set to false first so the correct username and password have to be entered for it to become true and allow them to login
        LoginFound = False

        'The screen has been cleared at the start of each menu so they look clearer
        Console.Clear()
        Console.WriteLine("                         Student Login")
        Console.WriteLine("                         =============")
        Console.WriteLine("")
        Console.WriteLine("Username: ")
        StudentUsername = Console.ReadLine()
        Console.WriteLine("")
        Console.WriteLine("")
        Console.WriteLine("Password: ")
        StudentPassword = Console.ReadLine()

        'The file has been closed here because I got an error saying it was already open and wouldn't run past this point. I have closed every file before opening them to prevent this from happening again.
        FileClose(1)
        FileOpen(1, path, OpenMode.Input)
        'Checks if username and password entered by the user match the ones in the file
        'Loops until the end of the file has been reached
        Do While Not EOF(1)
            UsernameCheck = LineInput(1)
            'Splits the strings in the file with commas to create fields
            Dim Fields As String() = UsernameCheck.Split(New Char() {","c})
            'Replaces any spaces in the file with nothing so they are not included when matching the correct characters
            Fields(0) = Replace(Fields(0), " ", "")
            If Fields(0) = StudentUsername Then
                Fields(3) = Replace(Fields(3), " ", "")
                If Fields(3) = StudentPassword Then
                    LoginFound = True
                    StudentMainMenu()
                Else
                    LoginFound = False
                End If
            End If
        Loop

        'This loop displays an error message if the user enters an incorrect username or password
        Do Until LoginFound = True
            If LoginFound = False Then
                Console.WriteLine("")
                Console.WriteLine("Either your username or password are invalid, please enter them again.")
                Console.WriteLine("Press enter to re-enter your username and password or press x to go back to the main menu")
                'The user can return to the main menu is they press x
                If Console.ReadLine = "x" Then
                    Main()
                End If
                FileClose(1)
                StudentLogin()
            End If
        Loop
        FileClose(1)
    End Sub
    'Sub where the teacher enters their username and password to login
    Sub TeacherLogin()
        'The comments would be the same as the student's login comments so I haven't repeated them here.
        'Initialise variables
        Dim TeacherPassword As String
        path = Directory.GetCurrentDirectory() & "\teacherlogindetails.txt"

        Console.Clear()
        Console.WriteLine("                         Teacher Login")
        Console.WriteLine("                         =============")
        Console.WriteLine("")
        Console.WriteLine("Username: ")
        TeacherUsername = Console.ReadLine()
        Console.WriteLine("")
        Console.WriteLine("")
        Console.WriteLine("Password: ")
        TeacherPassword = Console.ReadLine()

        FileClose(1)
        FileOpen(1, path, OpenMode.Input)
        Do While Not EOF(1)
            UsernameCheck = LineInput(1)
            Dim Fields As String() = UsernameCheck.Split(New Char() {","c})
            Fields(0) = Replace(Fields(0), " ", "")
            If Fields(0) = TeacherUsername Then
                Fields(3) = Replace(Fields(3), " ", "")
                If Fields(3) = TeacherPassword Then
                    LoginFound = True
                    TeacherMainMenu()
                Else
                    LoginFound = False
                End If
            End If
        Loop
        If LoginFound = False Then
            Console.WriteLine("")
            Console.WriteLine("Either your username or password are invalid, please enter them again.")
            Console.WriteLine("Press enter to re-enter your username and password or press x to go back to the main menu")
            If Console.ReadLine = "x" Then
                Main()
            End If
            FileClose(1)
            TeacherLogin()
        End If
        FileClose(1)
    End Sub
    'Student main menu
    Sub StudentMainMenu()
        Console.Clear()
        Console.WriteLine("                         Welcome to the Student")
        Console.WriteLine("                               Main Menu")
        Console.WriteLine("")
        Console.WriteLine("                         Would you like to:")
        Console.WriteLine("")
        Console.WriteLine("                         1. Take a test")
        Console.WriteLine("                         2. View past results")
        Console.WriteLine("                         3. Logout")
        Console.WriteLine("                         4. Quit")

        MenuChoice = Console.ReadLine
        'This loop allows the user to choose the option to either take a test, view their results, logout or quit.
        Do
            Select Case MenuChoice
                Case "1"
                    Testpathset()
                Case "2"
                    ViewingResults()
                Case "3"
                    Main()
                Case "4"
                    QuittingMenu()
                Case Else
                    Console.WriteLine("Enter a number between 1 and 4 to select your option")
                    MenuChoice = Console.ReadLine
                    Select Case MenuChoice
                        Case "1"
                            Testpathset()
                        Case "2"
                            ViewingResults()
                        Case "3"
                            Main()
                        Case "4"
                            QuittingMenu()
                    End Select
            End Select
        Loop
        Console.Read()
    End Sub
    'Teacher main menu
    Sub TeacherMainMenu()
        'The comments would be the same as the student main menu comments so I haven't repeated them here.
        Console.Clear()
        Console.WriteLine("                         Welcome to the Teacher")
        Console.WriteLine("                               Main Menu")
        Console.WriteLine("")
        Console.WriteLine("                         Would you like to:")
        Console.WriteLine("")
        Console.WriteLine("                         1. Add words and definitions")
        Console.WriteLine("                         2. View all students' details")
        Console.WriteLine("                         3. View all students' past results")
        Console.WriteLine("                         4. Logout")
        Console.WriteLine("                         5. Quit")

        MenuChoice = Console.ReadLine
        Do
            Select Case MenuChoice
                Case "1"
                    Testpathset()
                Case "2"
                    ViewingDetails()
                Case "3"
                    ViewingResults()
                Case "4"
                    Main()
                Case "5"
                    QuittingMenu()
                Case Else
                    Console.WriteLine("Enter a number between 1 and 5 to select your option")
                    MenuChoice = Console.ReadLine
                    Select Case MenuChoice
                        Case "1"
                            Testpathset()
                        Case "2"
                            ViewingDetails()
                        Case "3"
                            ViewingResults()
                        Case "4"
                            Main()
                        Case "5"
                            QuittingMenu()
                    End Select
            End Select
        Loop
        Console.Read()
    End Sub
    'Sub where the teacher creates the spelling test
    Sub CreateTest()
        'Initialise variables.
        Dim WordsDefinitions(0 To 9) As TAddWordandDefinition

        Console.Clear()
        Console.WriteLine("                    Add Words and Definitions")
        Console.WriteLine("                    =========================")
        Console.WriteLine("Enter the definition first, then the word")

        'Allows the user to enter 10 words and definitions.
        For LoopCounter = 0 To 9 Step 1
            Console.WriteLine((LoopCounter + 1) & "." & "Enter a definition:")
            WordsDefinitions(LoopCounter).Definition = Console.ReadLine

            Console.WriteLine("Enter a word")
            WordsDefinitions(LoopCounter).Word = Console.ReadLine
        Next
        Console.Clear()

        'Displays the definitions and words that the user has entered
        For LoopCounter = 0 To 9 Step 1
            Console.WriteLine("Definition: " & WordsDefinitions(LoopCounter).Definition)
            Console.WriteLine("Word: " & WordsDefinitions(LoopCounter).Word)
            'Converts the words to lowercase
            WordsDefinitions(LoopCounter).Word = StrConv(WordsDefinitions(LoopCounter).Word, VbStrConv.Lowercase)
        Next

        Console.WriteLine("")
        Console.WriteLine("You have sucessfully entered your words and definitions")
        Console.WriteLine("")
        Console.WriteLine("Would you like to: ")
        Console.WriteLine("1. Re-enter your results")
        Console.WriteLine("2. Continue and save the test ")

        MenuChoice = Console.ReadLine

        'This loop allows the user to choose the option of either re-entering the words and definitions or saving and going back to the teacher main menu
        Do
            Select Case MenuChoice
                Case "1"
                    CreateTest()
                Case "2"
                    FileClose(1)
                    FileOpen(1, path, OpenMode.Output)
                    For LoopCounter = 0 To 9 Step 1
                        Line = WordsDefinitions(LoopCounter).Definition + "," + WordsDefinitions(LoopCounter).Word
                        PrintLine(1, Line)
                    Next
                    Line = ""
                    PrintLine(1, Line)
                    FileClose(1)
                    Console.WriteLine("Your test has been saved, press enter to be redirected to the main menu")
                    Console.ReadLine()
                    TeacherMainMenu()
                Case Else
                    Console.WriteLine("Enter a number between 1 and 2 to select your option")
                    MenuChoice = Console.ReadLine
                    Select Case MenuChoice
                        Case "1"
                            CreateTest()
                        Case "2"
                            FileClose(1)
                            FileOpen(1, path, OpenMode.Output)
                            For LoopCounter = 0 To 9 Step 1
                                Line = WordsDefinitions(LoopCounter).Definition + "," + WordsDefinitions(LoopCounter).Word
                                PrintLine(1, Line)
                            Next
                            Line = ""
                            PrintLine(1, Line)
                            FileClose(1)
                            Console.WriteLine("Your test has been saved, press enter to be redirected to the main menu")
                            Console.ReadLine()
                            TeacherMainMenu()
                    End Select
            End Select
        Loop
        Console.Read()
    End Sub
    'Sub where the student takes the spelling test
    Sub TakeATest()
        'Initialise variables
        Dim WordsDefinitions(0 To 9) As TAddWordandDefinition
        Dim Answer(0 To 9), FeedBack As String
        Dim IndividualWordScore(0 To 9), LoopCounter, LoopCounter2, Score, RunningWordScore, CorrectPercentage As Integer
        Dim StudentUsernameFound As Boolean

        'Sets variables and counters to 0 or false
        LoopCounter = 0
        LoopCounter2 = 0
        Score = 0
        RunningWordScore = 0
        CorrectPercentage = 0
        StudentUsernameFound = False

        Console.Clear()
        Console.WriteLine("                    Take a test")
        Console.WriteLine("                    ===========")

        FileClose(1)
        FileOpen(1, path, OpenMode.Input)
        'Loops until the end of the file. Sets the words and definitions as different fields and stores them in variables
        Do While Not EOF(1)
            Test = LineInput(1)
            Dim Fields As String() = Test.Split(New Char() {","c})
            WordsDefinitions(LoopCounter).Definition = Fields(0)
            WordsDefinitions(LoopCounter).Word = Fields(1)
            LoopCounter = LoopCounter + 1
        Loop
        FileClose(1)

        'Displays the definition and allows the student to enter their spelling. This happens for each of the ten definitions
        For LoopCounter = 0 To 9
            Console.WriteLine("Definition: ")
            Console.WriteLine((LoopCounter + 1) & ". " & WordsDefinitions(LoopCounter).Definition)
            Console.WriteLine("Enter you answer: ")
            Answer(LoopCounter) = Console.ReadLine
        Next
        Console.WriteLine("The test is now complete, press enter to save and get your score.")

        'This loop marks each word the student has entered
        For LoopCounter = 0 To 9
            'Changes the answer the student has entered to lowercase to allow the words to be compared correctly
            Answer(LoopCounter) = StrConv(Answer(LoopCounter), VbStrConv.Lowercase)
            RunningWordScore = 0
            'If the two words match then they get full marks for that word (2 marks)
            If Answer(LoopCounter) = WordsDefinitions(LoopCounter).Word Then
                Score = Score + 2
                IndividualWordScore(LoopCounter) = IndividualWordScore(LoopCounter) + 2
            Else
                'If they don't match then the words are split and each letter is checked
                For LoopCounter2 = 1 To Len(Answer(LoopCounter))
                    If Mid(Answer(LoopCounter), LoopCounter2, 1) = Mid(WordsDefinitions(LoopCounter).Word, LoopCounter2, 1) Then
                        'For Each correct letter, 1 is added to the RunningWordScore
                        RunningWordScore = RunningWordScore + 1
                    End If
                Next
                'Works out what percentage of the word they got correct by dividing the runningwordscore by the longest word
                If Answer(LoopCounter) < WordsDefinitions(LoopCounter).Word Then
                    CorrectPercentage = (RunningWordScore / Len(WordsDefinitions(LoopCounter).Word)) * 100
                End If
                If Answer(LoopCounter) > WordsDefinitions(LoopCounter).Word Then
                    CorrectPercentage = (RunningWordScore / Len(Answer(LoopCounter))) * 100
                End If
                'The score is then worked out by what percentage of the word they got correct
                If CorrectPercentage >= 50 Then
                    Score = Score + 1
                    IndividualWordScore(LoopCounter) = IndividualWordScore(LoopCounter) + 1
                End If
            End If
        Next
        FileClose(1)

        'Works out the feedback to be displayed at the end of the test, based on the score
        If Score >= 20 Then
            FeedBack = "Congratulations, you scored full marks, keep up the good work."
        ElseIf Score <= 9 Then
            FeedBack = "Some more practice is needed."
        Else
            FeedBack = "Well done."
        End If
        Console.Read()

        'Saves the score to the temporary file to allow new scores to be added to the correct line
        path = Directory.GetCurrentDirectory() & "\studentresults.txt"
        path2 = Directory.GetCurrentDirectory() & "\tempfile.txt"
        FileOpen(1, path, OpenMode.Input)
        FileOpen(2, path2, OpenMode.Output)
        Do Until EOF(1)
            Line = LineInput(1)
            Dim Fields As String() = Line.Split(New Char() {","c})
            If Fields(0) = StudentUsername Then
                StudentUsernameFound = True
                Line = Line + ", " + CStr(Score)
            Else
                StudentUsernameFound = False
            End If
            PrintLine(2, Line)
        Loop
        FileClose(1)
        FileClose(2)

        'Deletes the origional student results file and renames the temporary file to the student results file
        My.Computer.FileSystem.DeleteFile(Directory.GetCurrentDirectory() & "\studentresults.txt")
        Rename(Directory.GetCurrentDirectory() & "\tempfile.txt", Directory.GetCurrentDirectory() & "\studentresults.txt")

        'Displays the definition, correct word, the students answer and their score for each word of the test
        Console.Clear()
        Console.WriteLine("")
        Console.WriteLine("Your results: ")
        Console.WriteLine("=============")
        For LoopCounter = 0 To 9
            Console.WriteLine("")
            Console.WriteLine((LoopCounter + 1) & ". " & "Definition: " & WordsDefinitions(LoopCounter).Definition)
            Console.WriteLine("Correct answer: " & WordsDefinitions(LoopCounter).Word)
            Console.WriteLine("Your answer: " & Answer(LoopCounter))
            Console.WriteLine("Your score for this answer: " & IndividualWordScore(LoopCounter))
        Next
        'Displays the total score for the test and some feedback
        Console.WriteLine("")
        Console.WriteLine("Your total score is: " & Score & "/20")
        Console.WriteLine(FeedBack)
        Console.WriteLine("")
        Console.WriteLine("")
        Console.WriteLine("Would you like to: ")
        Console.WriteLine("1. Go to the student main menu?")
        Console.WriteLine("2. Logout? ")

        MenuChoice = Console.ReadLine

        'This loop allows the user to choose the option of either returning to the student main menu or logging out
        Do
            Select Case MenuChoice
                Case "1"
                    StudentMainMenu()
                Case "2"
                    Main()
                Case Else
                    Console.WriteLine("Enter a number between 1 and 2 to select your option")
                    MenuChoice = Console.ReadLine
                    Select Case MenuChoice
                        Case "1"
                            StudentMainMenu()
                        Case "2"
                            Main()
                    End Select
            End Select
        Loop
        Console.Read()
    End Sub
    'Sub where the teacher view students' details
    Sub ViewingDetails()
        Console.Clear()
        'Initialise variables
        Dim StudentUsernameSearch As String
        Dim StudentUsernameFound As Boolean
        StudentUsernameFound = False
        Console.Clear()
        Console.WriteLine("                     Searching for Details")
        Console.WriteLine("                     =====================")
        Console.WriteLine("")
        Console.WriteLine("Enter the username of the student that you want to search for: ")

        StudentUsernameSearch = Console.ReadLine()

        'Reads from the details file
        path = Directory.GetCurrentDirectory() & "\studentlogindetails.txt"
        FileClose(1)
        FileOpen(1, path, OpenMode.Input)
        Do While Not EOF(1)
            UsernameCheck = LineInput(1)
            Dim Fields As String() = UsernameCheck.Split(New Char() {","c})
            Fields(0) = Replace(Fields(0), " ", "")
            If Fields(0) = StudentUsernameSearch Then
                StudentUsernameFound = True
                Console.WriteLine(UsernameCheck)
            End If
        Loop
        FileClose(1)
        'Displays an error message if the username they have entered is invalid
        If StudentUsernameFound = False Then
            Console.WriteLine("The username you have entered is invalid.")
        End If
        'If the user presses x then they will return to the main menu
        Console.WriteLine("")
        Console.WriteLine("Press enter to re-enter your search or press x to go back to the main menu.")
        If Console.ReadLine = "x" Then
            TeacherMainMenu()
        End If
        ViewingDetails()
    End Sub
    'Sub where the file paths are set based on the users choice
    Sub Testpathset()
        'This menu allows the teacher to choose which yeah group they want to make a test for.
        Console.Clear()
        Console.WriteLine("                       Choose a year group")
        Console.WriteLine("                       ===================")
        Console.WriteLine("")

        'Displays different things based on who is logged in
        If StudentUsername = "" Then
            Console.WriteLine("What year group would you like to create a test for?: ")
        Else
            Console.WriteLine("What year group are you in?")
        End If
        Console.WriteLine("1. Year 3")
        Console.WriteLine("2. Year 4")
        Console.WriteLine("3. Year 5")
        Console.WriteLine("4. Year 6")
        Console.WriteLine("5. Back to main menu")

        MenuChoice = Console.ReadLine
        'This loop changes the path based on which option they pick
        If StudentUsername = "" Then
            Do
                Select Case MenuChoice
                    Case "1"
                        path = Directory.GetCurrentDirectory() & "\createtestyear3.txt"
                        CreateTest()
                    Case "2"
                        path = Directory.GetCurrentDirectory() & "\createtestyear4.txt"
                        CreateTest()
                    Case "3"
                        path = Directory.GetCurrentDirectory() & "\createtestyear5.txt"
                        CreateTest()
                    Case "4"
                        path = Directory.GetCurrentDirectory() & "\createtestyear6.txt"
                        CreateTest()
                    Case "5"
                        TeacherMainMenu()
                    Case Else
                        Console.WriteLine("Enter a number between 1 and 5 to select your option")
                        MenuChoice = Console.ReadLine
                        Select Case MenuChoice
                            Case "1"
                                path = Directory.GetCurrentDirectory() & "\createtestyear3.txt"
                                CreateTest()
                            Case "2"
                                path = Directory.GetCurrentDirectory() & "\createtestyear4.txt"
                                CreateTest()
                            Case "3"
                                path = Directory.GetCurrentDirectory() & "\createtestyear5.txt"
                                CreateTest()
                            Case "4"
                                path = Directory.GetCurrentDirectory() & "\createtestyear6.txt"
                                CreateTest()
                            Case "5"
                                TeacherMainMenu()
                        End Select
                End Select
            Loop
        Else
            Do
                Select Case MenuChoice
                    Case "1"
                        path = Directory.GetCurrentDirectory() & "\createtestyear3.txt"
                        TakeATest()
                    Case "2"
                        path = Directory.GetCurrentDirectory() & "\createtestyear4.txt"
                        TakeATest()
                    Case "3"
                        path = Directory.GetCurrentDirectory() & "\createtestyear5.txt"
                        TakeATest()
                    Case "4"
                        path = Directory.GetCurrentDirectory() & "\createtestyear6.txt"
                        TakeATest()
                    Case "5"
                        StudentMainMenu()
                    Case Else
                        Console.WriteLine("Enter a number between 1 and 5 to select your option")
                        MenuChoice = Console.ReadLine
                        Select Case MenuChoice
                            Case "1"
                                path = Directory.GetCurrentDirectory() & "\createtestyear3.txt"
                                TakeATest()
                            Case "2"
                                path = Directory.GetCurrentDirectory() & "\createtestyear4.txt"
                                TakeATest()
                            Case "3"
                                path = Directory.GetCurrentDirectory() & "\createtestyear5.txt"
                                TakeATest()
                            Case "4"
                                path = Directory.GetCurrentDirectory() & "\createtestyear6.txt"
                                TakeATest()
                            Case "5"
                                StudentMainMenu()
                        End Select
                End Select
            Loop
        End If
    End Sub
    'Sub where the user can view the results for the test
    Sub ViewingResults()
        Dim StudentUsernameSearch As String
        Dim StudentUsernameFound As Boolean
        Dim LoopCounter, ArrayLength As Integer
        path = Directory.GetCurrentDirectory() & "\studentresults.txt"
        Console.Clear()
        Console.WriteLine("                           Results")
        Console.WriteLine("                           =======")
        Console.WriteLine("")
        StudentUsernameFound = True

        'Displays different things based on which user is logged in
        If TeacherUsername = "" Then
            Console.WriteLine("Here are your results:")
            Console.WriteLine("")
            'Reads from the results file
            FileClose(1)
            FileOpen(1, path, OpenMode.Input)
            Do While Not EOF(1)
                UsernameCheck = LineInput(1)
                Dim Fields As String() = UsernameCheck.Split(New Char() {","c})
                Fields(0) = Replace(Fields(0), " ", "")
                'If the student username matches then their results are displayed
                If Fields(0) = StudentUsername Then
                    ArrayLength = Fields.Length
                    ArrayLength = ArrayLength - 2
                    For LoopCounter = 0 To ArrayLength
                        If LoopCounter > 8 Then
                            Console.WriteLine("Test " & LoopCounter + 1 & ":" & Fields(LoopCounter + 1) & "/20")
                        Else
                            Console.WriteLine("Test " & LoopCounter + 1 & ":" & vbTab & Fields(LoopCounter + 1) & "/20")
                        End If
                    Next
                End If
            Loop
            'Allows the user to return to the main menu
            Console.WriteLine("")
            Console.WriteLine("Press enter to go back to the main menu.")
            Console.ReadLine()
            FileClose(1)
            StudentMainMenu()
        Else
            Console.Clear()
            Console.WriteLine("                           Results")
            Console.WriteLine("                           =======")
            Console.WriteLine("Would you like to: ")
            Console.WriteLine("")
            Console.WriteLine("1. View all students' results")
            Console.WriteLine("2. Search for a specific student")
            Console.WriteLine("3. Back to main menu")

            MenuChoice = Console.ReadLine

            'This option allows them to choose from viewing all student results or searcing for a specific student
            Do
                Select Case MenuChoice
                    Case "1"
                        Console.Clear()
                        Console.WriteLine("                           Results")
                        Console.WriteLine("                           =======")
                        Console.WriteLine("")
                        'Reads from the results file
                        FileClose(1)
                        FileOpen(1, path, OpenMode.Input)
                        Do While Not EOF(1)
                            UsernameCheck = LineInput(1)
                            Dim Fields As String() = UsernameCheck.Split(New Char() {","c})
                            Fields(0) = Replace(Fields(0), " ", "")
                            Console.WriteLine(UsernameCheck)
                        Loop
                        Console.WriteLine("Press enter to go back to the main menu")
                        Console.ReadLine()
                        TeacherMainMenu()
                    Case "2"
                        Console.Clear()
                        Console.WriteLine("                           Results")
                        Console.WriteLine("                           =======")
                        Console.WriteLine("")
                        Console.WriteLine("Enter the username of the student that you want to search for: ")
                        Console.WriteLine("")
                        'Allows the teacher to search for which student's results they want to see
                        StudentUsernameSearch = Console.ReadLine()
                        'Reads from the results file
                        FileClose(1)
                        FileOpen(1, path, OpenMode.Input)
                        StudentUsernameFound = False
                        Do While Not EOF(1)
                            UsernameCheck = LineInput(1)
                            Dim Fields As String() = UsernameCheck.Split(New Char() {","c})
                            Fields(0) = Replace(Fields(0), " ", "")
                            If Fields(0) = StudentUsernameSearch Then
                                ArrayLength = Fields.Length
                                ArrayLength = ArrayLength - 2
                                'Results are aligned correctly based on number of didgets in the test number
                                For LoopCounter = 0 To ArrayLength
                                    If LoopCounter > 8 Then
                                        Console.WriteLine("Test " & LoopCounter + 1 & ":" & Fields(LoopCounter + 1) & "/20")
                                    Else
                                        Console.WriteLine("Test " & LoopCounter + 1 & ":" & vbTab & Fields(LoopCounter + 1) & "/20")
                                    End If
                                    StudentUsernameFound = True
                                Next
                            End If
                        Loop
                        FileClose(1)
                        'If the username in the file doesn't match the username they have seared for then an error message is displayed
                        If StudentUsernameFound = False Then
                            Console.WriteLine("The username you have entered is invalid.")
                        End If
                        Console.WriteLine("")
                        Console.WriteLine("Press enter to go back to the menu.")
                        Console.ReadLine()
                        ViewingResults()
                    Case "3"
                        TeacherMainMenu()
                    Case Else
                        Console.WriteLine("Enter a number between 1 and 5 to select your option")
                        MenuChoice = Console.ReadLine
                        Select Case MenuChoice
                            Case "1"
                                Console.Clear()
                                Console.WriteLine("                           Results")
                                Console.WriteLine("                           =======")
                                Console.WriteLine("")
                                'Reads from the results file
                                FileClose(1)
                                FileOpen(1, path, OpenMode.Input)
                                Do While Not EOF(1)
                                    UsernameCheck = LineInput(1)
                                    Dim Fields As String() = UsernameCheck.Split(New Char() {","c})
                                    Fields(0) = Replace(Fields(0), " ", "")
                                    Console.WriteLine(UsernameCheck)
                                Loop
                                Console.WriteLine("Press enter to go back to the main menu")
                                Console.ReadLine()
                                TeacherMainMenu()
                            Case "2"
                                Console.Clear()
                                Console.WriteLine("                           Results")
                                Console.WriteLine("                           =======")
                                Console.WriteLine("")
                                Console.WriteLine("Enter the username of the student that you want to search for: ")
                                Console.WriteLine("")
                                'Allows the teacher to search for which student's results they want to see
                                StudentUsernameSearch = Console.ReadLine()
                                'Reads from the results file
                                FileClose(1)
                                FileOpen(1, path, OpenMode.Input)
                                StudentUsernameFound = False
                                Do While Not EOF(1)
                                    UsernameCheck = LineInput(1)
                                    Dim Fields As String() = UsernameCheck.Split(New Char() {","c})
                                    Fields(0) = Replace(Fields(0), " ", "")
                                    If Fields(0) = StudentUsernameSearch Then
                                        ArrayLength = Fields.Length
                                        ArrayLength = ArrayLength - 2
                                        'Results are aligned correctly based on number of didgets in the test number
                                        For LoopCounter = 0 To ArrayLength
                                            If LoopCounter > 8 Then
                                                Console.WriteLine("Test " & LoopCounter + 1 & ":" & Fields(LoopCounter + 1) & "/20")
                                            Else
                                                Console.WriteLine("Test " & LoopCounter + 1 & ":" & vbTab & Fields(LoopCounter + 1) & "/20")
                                            End If
                                            StudentUsernameFound = True
                                        Next
                                    End If
                                Loop
                                FileClose(1)
                                'If the username in the file doesn't match the username they have seared for then an error message is displayed
                                If StudentUsernameFound = False Then
                                    Console.WriteLine("The username you have entered is invalid.")
                                End If
                                Console.WriteLine("")
                                Console.WriteLine("Press enter to go back to the menu.")
                                Console.ReadLine()
                                ViewingResults()
                            Case "3"
                                TeacherMainMenu()
                        End Select
                End Select
            Loop
        End If
    End Sub
    'Sub where the user can choose to either quit the system or return to the appropriate menu
    Sub QuittingMenu()
        Console.Clear()
        Console.WriteLine("                Are you sure you want to quit the system?")
        Console.WriteLine("")
        Console.WriteLine("             Enter the number of the option you want to pick:")
        Console.WriteLine("")
        Console.WriteLine("                    1. Yes, I want to quit the system.")
        Console.WriteLine("                    2. No, I want to return to the main menu.")

        MenuChoice = Console.ReadLine
        'Displays different choices based on whether the user is logged in as student or teacher
        If StudentUsername = "" And TeacherUsername = "" Then
            Do
                Select Case MenuChoice
                    Case "1"
                        End
                    Case "2"
                        Main()
                    Case Else
                        Console.WriteLine("Enter a number between 1 and 2 to select your option")
                        MenuChoice = Console.ReadLine
                        Select Case MenuChoice
                            Case "1"
                                End
                            Case "2"
                                Main()
                        End Select
                End Select
            Loop
        ElseIf StudentUsername = "" Then
            Do
                Select Case MenuChoice
                    Case "1"
                        End
                    Case "2"
                        TeacherMainMenu()
                    Case Else
                        Console.WriteLine("Enter a number between 1 and 2 to select your option")
                        MenuChoice = Console.ReadLine
                        Select Case MenuChoice
                            Case "1"
                                End
                            Case "2"
                                TeacherMainMenu()
                        End Select
                End Select
            Loop
        Else
            Do
                Select Case MenuChoice
                    Case "1"
                        End
                    Case "2"
                        StudentMainMenu()
                    Case Else
                        Console.WriteLine("Enter a number between 1 and 2 to select your option")
                        MenuChoice = Console.ReadLine
                        Select Case MenuChoice
                            Case "1"
                                End
                            Case "2"
                                StudentMainMenu()
                        End Select
                End Select
            Loop
        End If
    End Sub
End Module