
5/15/2022  Joseph DiPierro
https://github.com/restaurant365/challenge-calculator
All requirements and stretch goals completed.
https://github.com/joedeveloperaspnet/challenge-calculator
Default arguements can be overwritten by arguements
passed into the main function or entered on the command line.
Each arguement is uniquely identified by it's type.

Sample Output:

 Available Arguements
----------------------------------------------------
 Deny negative numbers (boolean) default = False
 Upper bound (integer) default = 1000
 Alturnate delimiter (string) predefined = , \n
 For example:  |?| 100 True
----------------------------------------------------
 No console arguements specified
----------------------------------------------------

Enter arguement(s) (optional): joe 2022 True

Alturnate delimiter = joe
Upper bound = 2022
Deny negative numbers = True

----------------------------------------------------------------
 Numbers can be entered directly, comma separated, or
 in this format:  //[{delimiter1}][{delimiter2}]...\n{numbers}
 Valid operator types are:  +  -  /  *
 Enter Ctrl + C when finished.
----------------------------------------------------------------

Enter operator type: + Addition
Enter delimeters/numbers input: 100,200joe300
Formula: 100 + 200 + 300 = 600

Enter operator type: - Subtraction
Enter delimeters/numbers input: 1000joe500,300
Formula: 1000 - 500 - 300 = 200

Enter operator type: * Multiplication
Enter delimeters/numbers input: 10joe10,3
Formula: 10 * 10 * 3 = 300

Enter operator type: / Division
Enter delimeters/numbers input: 27joe3,3
Formula: 27 / 3 / 3 = 3

Enter operator type: $
Error: Invalid operator type!

Enter operator type: @
Error: Invalid operator type!

Enter operator type:
Finished


All Test Cases Passed

Press enter key to exit
