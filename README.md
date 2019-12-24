
# Driving test management system
The full and original Hebrew version of this file is available [here](Screenshots/%D7%AA%D7%99%D7%A7%20%D7%A4%D7%A8%D7%95%D7%99%D7%A7%D7%98.pdf).
## Introduction
The software is designed to allow a driving student to register for the test without being dependent on their teacher.
The student selects the date, time, and starting position at which he wants the test, and the software finds him a suitable tester from the pool of available testers according to the constraints requested by the student.

## Description of screens and system activity
### Screen Flow Diagram
![enter image description here](https://github.com/nmanor/DrivingTestManagementSystem2019/blob/master/Screenshots/Screen%20flow.png?raw=true)
### The main screen
The main screen is divided into 3 areas:

 1. The student area, where you can register as a new student or login as a student who is already enrolled in the system.
2. The tester area, where you can register as a new tester or login as a tester already registered in the system.
3. The system worker area, where you can connect with a pre-recognized password as a system employee who has control over everything.
Both login as a student and login as examiner, you can view the password itself (instead of the ● marks) by pressing the "show password" button. In addition, you can login by pressing the enter key and not necessarily by pressing the "sign in" button).
![enter image description here](https://github.com/nmanor/DrivingTestManagementSystem2019/blob/master/Screenshots/Main%20Screen.png?raw=true)

### Student screen
The student screen can be in one of two modes:
1. A condition where the student has already passed a theory test and can register for the test and watch previous tests
2. A situation in which the student has not yet passed a theory, and then the student can take a test and / or update his / her personal information

#### "Update details" tab

This screen is the same for a student who has passed a theory test and for a student who has not yet passed. In this screen the student can update his or her personal details and even delete himself from the system.
The student can enter the number of lessons he or she does or download and upload by pressing the + and - buttons.
![enter image description here](https://github.com/nmanor/DrivingTestManagementSystem2019/blob/master/Screenshots/Update%20info.png?raw=true)
#### "Theory Test" tab
This screen allows the student to perform a theory test based on predefined random theory questions stored in the system, for a limited time. If the student has passed, he or she will be able to register immediately for the test and if he does not have to wait 24 hours until the next test is taken.
The student can move from question to question, or skip to a specific question by clicking on the question number on the top left of the screen (in the second image).
![enter image description here](https://github.com/nmanor/DrivingTestManagementSystem2019/blob/master/Screenshots/Theary%20test%201.png?raw=true)
![enter image description here](https://github.com/nmanor/DrivingTestManagementSystem2019/blob/master/Screenshots/Theary%20test%202.png?raw=true)
#### "Register for Test" tab
In this screen (which appears only in a student who has passed theory) you can register for the test according to the constraints chosen by the student. At the end of the process, the student will receive an email detailing the test details and will be offered the option to add the test to their Google Calendar with all the details of the test along with the date of the test and other information.
By clicking the "Set the start point as my home" button, the student can set the address as his or her home address with which he or she is registered, rather than typing it himself.
![enter image description here](https://github.com/nmanor/DrivingTestManagementSystem2019/blob/master/Screenshots/Test%20registration.png?raw=true)
![enter image description here](https://github.com/nmanor/DrivingTestManagementSystem2019/blob/master/Screenshots/Test%20registration%20calander.png?raw=true)
![enter image description here](https://github.com/nmanor/DrivingTestManagementSystem2019/blob/master/Screenshots/Test%20registration%20email.png?raw=true)
#### The "Your Tests" tab
This screen is divided into 3 sections:
1. Tests where the student has passed, where he can view all the details of all the tests on all types of vehicles in which he has passed a test. A mouse pass on a test that the student passed will have a button that allows him to run an algorithm that creates a license fee specifically tailored to him and containing his identity number, his name, the type of vehicle he passed the test and the expiration date of the fee, which is set exactly 3 months from the date he passed. test. With this bag, the student can go to the mail and pay it, much like a real toll. The resulting fee looks like this:
![enter image description here](https://github.com/nmanor/DrivingTestManagementSystem2019/blob/master/Screenshots/Fee.png?raw=true)
2. The tests where the student fails. Same as the test view he passed but will show him the details of the tests he failed.
3. Future Tests, where the tests that have not yet been and / or the tests on which the examiner has not yet entered the test score will be displayed, and delete a test if he wishes to cancel it.
![enter image description here](https://github.com/nmanor/DrivingTestManagementSystem2019/blob/master/Screenshots/User%20tests.png?raw=true)

### Tester screen
#### "Update details" tab
In this screen, the examiner can update his personal details and even delete himself from the system.
![enter image description here](https://github.com/nmanor/DrivingTestManagementSystem2019/blob/master/Screenshots/Update%20tester%20info.png?raw=true)

#### "Weekly System" tab
On this screen, the examiner can watch his weekly hours system, and switch between the different weeks.
![enter image description here](https://github.com/nmanor/DrivingTestManagementSystem2019/blob/master/Screenshots/Weekly%20schedule.png?raw=true)
If the student erased himself or a malfunction occurred, the red rectangle tester will be shown.
![enter image description here](https://github.com/nmanor/DrivingTestManagementSystem2019/blob/master/Screenshots/Red%20rectangle.png?raw=true)
#### "Your Tests" tab
On this screen, the examiner can view tests that are linked to them in a sorted way:
1. Existing tests that must be graded.
2. Tests on which he has already scored in the past. It can even run such a test (in case of an appeal).
3. Future tests that have not yet been deleted can be deleted in case of illness and the like.
![enter image description here](https://github.com/nmanor/DrivingTestManagementSystem2019/blob/master/Screenshots/Tester%20tests.png?raw=true)

### System workers screen
#### "Student View" tab
In this screen, the employee can view the list of all students stored in the system and even search for students by first name, last name or identity number (sorted search will appear immediately). By double-clicking on the student's name, you can even log in as the same student, and watch his / her screen that he sees when connecting. For example, if the student forgets the password, the system employee can create a new password for him directly from his screen.
When searching, you can click the small X button on the right side of the search bar to cancel the search.
![enter image description here](https://github.com/nmanor/DrivingTestManagementSystem2019/blob/master/Screenshots/Student%20tab.png?raw=true)

#### "Test View" tab
In this screen, the employee can view the list of all testers stored in the system and even search for testers by first name, last name or identity number (sorted search will appear immediately). By double clicking on the tester name you can even connect as the same tester, and watch his screen which he sees when you log in. For example, if the tester forgets the password, the system employee can create a new password for him directly from his screen.
When searching, you can click the small X button on the right side of the search bar to cancel the search.
![enter image description here](https://github.com/nmanor/DrivingTestManagementSystem2019/blob/master/Screenshots/Testers%20tab.png?raw=true)

#### "Queries and Statistics" tab
On this screen, the employee can view various graphs that show him information about the data stored in the system. Each graph below has the information in the form of a tree, so you can see, for example, all the information of a student attending a "Wheels" school from the graph of dividing students by schools.
![enter image description here](https://github.com/nmanor/DrivingTestManagementSystem2019/blob/master/Screenshots/Statistics%20tab.png?raw=true)

#### "Settings" tab
This screen is divided into 2 parts:
1. System Value Definitions, where the employee can change and view different values that affect the behavior of the system, such as the minimum tester age, the maximum tester age, and the number of questions in a theory test.
2. The theory questions, where the employee can watch questions, add questions and delete some questions at the same time. You can see if a specific question contains an image, and see the correct answer for that question.
![enter image description here](https://github.com/nmanor/DrivingTestManagementSystem2019/blob/master/Screenshots/Settings%20tab.png?raw=true)

### More screens
#### Add student screen
This screen appears when you click on the student's enrollment button on the main screen, where the student can enter their personal details and save themselves in the system. At the end of the process, the student will receive an e-mail stating that he / she has successfully registered with his / her personal password, in addition the system will encrypt the student's personal password and store it in an encoded form so that no password can be viewed from an external source (eg via the XML file containing all students).
For convenience, we chose not to check the correctness of the identity number entered by the user in order to allow ourselves and the examiner to enter a simple identity number without having to enter a real number. However, the identity number must contain exactly 9 digits.
![enter image description here](https://github.com/nmanor/DrivingTestManagementSystem2019/blob/master/Screenshots/New%20student.png?raw=true)
![enter image description here](https://github.com/nmanor/DrivingTestManagementSystem2019/blob/master/Screenshots/New%20student%20email.png?raw=true)

#### Tester Add Screen
This screen appears when you press the tester's enrollment button on the main screen, where the tester can enter his personal information and save himself in the system. Upon enrollment, the system encodes the tester's personal password and stores it in a coded way, so that no password can be viewed from an external source (for example, through the XML file containing all the testers).
In this screen, the examiner can mark the hours he works by using an incremental table where he marks which days and what hours he works and when not, by clicking on the appropriate hour.
For convenience, we chose not to check the correctness of the identity number entered by the user in order to allow ourselves and the examiner to enter a simple identity number without having to enter a real number. However, the identity number must contain exactly 9 digits.
Theoretically, if the software was actually working in the market, of course, it is necessary to add a brief check and make sure that the user's registered record is indeed a test taker's in the Licensing Office's internal examiner database, in order to prevent users who are not officially licensing examiners from registering .
![enter image description here](https://github.com/nmanor/DrivingTestManagementSystem2019/blob/master/Screenshots/New%20tester.png?raw=true)

#### Test update screen
This screen allows the tester to rate the student according to their performance in the test. In each criterion, the examiner can mark the student's level on the same metric (poor, reasonable or good) and receive an automatic recommendation from the system according to the algorithm whether or not to pass the student.
Access to this screen is done by moving a mouse on a test screen inside the examiner's screen and pressing the round edit button, if the test allows it (so the test cannot be taken yet).<br>
![enter image description here](https://github.com/nmanor/DrivingTestManagementSystem2019/blob/master/Screenshots/Update%20test%201.png?raw=true)
![enter image description here](https://github.com/nmanor/DrivingTestManagementSystem2019/blob/master/Screenshots/Update%20test%202.png?raw=true)

#### Add test screen
On this screen, the system employee can add a theory question to the repository by clicking the button
![enter image description here](https://github.com/nmanor/DrivingTestManagementSystem2019/blob/master/Screenshots/New%20question.png?raw=true)
The insert is made by entering the question itself, the correct answer, and 3 incorrect answers. She returned to the picture.<br>
![enter image description here](https://github.com/nmanor/DrivingTestManagementSystem2019/blob/master/Screenshots/New%20question%20edit%201.png?raw=true)
![enter image description here](https://github.com/nmanor/DrivingTestManagementSystem2019/blob/master/Screenshots/New%20question%20edit%202.png?raw=true)


## Appendices

##### This is a partial explanation of this appendix. For a more detailed explanation in Hebrew [click here](Screenshots/%D7%AA%D7%99%D7%A7%20%D7%A4%D7%A8%D7%95%D7%99%D7%A7%D7%98.pdf).
 Overall the system stores all the data it uses in 5 different XML files.

### Appendix 1 - The XML files of the data
Overall the system stores all the data it uses in 5 different XML files.

#### Student file - TraineePath
In this file, all students are saved in the following format:
```xml
<Traniee>
	<ID>987654321</ID>
	<Last_name>ישראלי</Last_name>
	<First_name>ישראל</First_name>
	<Birth_date>
	<Day>20</Day>
	<Month>8</Month>
	<Year>1999</Year>
	</Birth_date>
	<Gender>0</Gender>
	<Phone>087654673</Phone>
	<Address>
		<City>ירושלים</City>
		<Building_number>21</Building_number>
		<Street>הועד הלאומי</Street>
	</Address>
	<Pass_theory>True</Pass_theory>
	<Last_theory>22/01/2019 13:18:56</Last_theory>
	<Gear_used>0</Gear_used>
	<Learned>0</Learned>
	<Driving_school>נוסעים על בטוח</Driving_school>
	<Teachers_name>עומרי</Teachers_name>
	<Num_of_classes>21</Num_of_classes>
	<Password>B7sHVwfzB</Password>
	<Email>example@gmail.com</Email>
</Traniee>
```

#### Tester File - TesterPath
In this file, all examiners are saved in the following format:
```xml
<Tester>
	<ID>123456789</ID>
	<Last_name>ישראלי</Last_name>
	<First_name>ישראל</First_name>
	<Birth_date>
		<Day>11</Day>
		<Month>11</Month>
		<Year>1965</Year>
	</Birth_date>
	<Gender>0</Gender>
	<Phone>0529872564</Phone>
	<Address>
		<City>חיפה</City>
		<Building_number>4</Building_number>
		<Street>ז'בוטינסקי</Street>
	</Address>
	<Years_of_experience>32</Years_of_experience>
	<Specialization>0</Specialization>
	<Schedule>
		111111011001101110110111110111
	</Schedule>
	<Maximum_distance>9</Maximum_distance>
	<Maximum_tests>11</Maximum_tests>
	<Password>Bg5JmcFH4</Password>
</Tester>
```

#### Test File - TestPath
In this file all tests are saved: future tests that have not yet received an answer from the examiner, and tests that have already received an answer and a grade from the examiner.
Test XElement format before getting your answer:
```xml
<Test>
	<Test_number>12CFB57O</Test_number>
	<Tester_id>123456789</Tester_id>
	<Traniee_id>987654321</Traniee_id>
	<Test_time>
		<Year>2019</Year>
		<Month>3</Month>
		<Day>25</Day>
		<Hour>13</Hour>
	</Test_time>
	<Address>
		<City>בית שמש</City>
		<Building_number>3</Building_number>
		<Street>נחל נועם</Street>
	</Address>
	<Criteria_list/>
	<Grade>False</Grade>
	<Tester_comment/>
	<Student_car_Type>0</Student_car_Type>
</Test>
```
Test XElement format after getting the answer:
```xml
<Test>
	<Test_number>A2B4FF8E</Test_number>
	<Tester_id>123456789</Tester_id>
	<Traniee_id>123456789</Traniee_id>
	<Test_time>
		<Year>2019</Year>
		<Month>1</Month>
		<Day>24</Day>
		<Hour>11</Hour>
	</Test_time>
	<Address>
		<City>ירושלים</City>
		<Building_number>5</Building_number>
		<Street>הלר</Street>
	</Address>
	<Criteria_list>
		<Criterion>
			<Score>2</Score>
			<Description>ציות לרמזורים</Description>
		</Criterion>
	</Criteria_list>
	<Grade>True</Grade>
	<Tester_comment>כל הכבוד</Tester_comment>
	<Student_car_Type>0</Student_car_Type>
</Test>
```

#### Theory questions file - TheoryPath
In this file all theory questions are saved in the following format:
```xml
<TheoryQuestion>
	<Question>מה פירוש התמרור?</Question>
	<Answer>חרוט להכוונה או לסימון מכשול במקום של עבודות בדרך</Answer>
	<Wrong>הכביש הולך וצר@
			קצה קטע של דרך הררית@
			מסמן מקום עצירה לרענון של נהגים. חובה להדליק אור צהוב או אור אדום מהבהב
	</Wrong>
	<Image_code> Image code… <Image_code/>
</TheoryQuestion>
```

#### Configuration file - ConfigurationPath
This file saves different values that the system uses in the following format:
```xml
<Configuration>
	<Timer_theory>30</Timer_theory>
	<Amount_questions>4</Amount_questions>
	<Fail_theory>1</Fail_theory>
	<Minimun_lessons>20</Minimun_lessons>
	<Maximum_tester_age>75</Maximum_tester_age>
	<Minimum_tester_age>40</Minimum_tester_age>
	<Minimum_Trainee_age>16</Minimum_Trainee_age>
	<Time_between_tests>7</Time_between_tests>
	<Test_serial_number>11</Test_serial_number>
	<Pass_test>2</Pass_test>
	<Defualt_distance>5</Defualt_distance>
	<Worker_password>worker</Worker_password>
</Configuration>
```

### Appendix 2 - Typical Struct and special enum variables
The system has a unique structure that specifies a single criterion. Each test instance contains a `List<Criterion>` list that specifies the criteria in that test.
The Struct is defined as follows:
```c#
public  struct  Criterion
{
	// Criterion fields: Result, and strings that describe the criterion itself
	public  Score Score;
	public  string Description;
	
	// Simple constructor that gets result and description
	public Criterion(Score _score, string _description)
	{
		Score = _score;
		Description = _description;
	}
	
	// Override the print function
	public  override  string ToString()
	{ 
		return  "בקריטריון " + Description + " קיבל  התלמיד  ציון  של " + ((int)Score + 1) + "/3"; }
	}
```
In addition, the project contains 2 enum variables that we created to facilitate system programming, apart from the basic enum required in the project conditions.
An enum variable named Thery_status that reflects the student's condition in terms of the theory test as one of 3 states:
```c#
public  enum  Thery_status
{
	// The student has already passed a theory test
	Passed,

	// The student has not yet passed a theory test, and he may take one
	NeedToDo,

	// The student has not yet passed a theory test but he is unable to access at the moment because it has not been long enough since he failed the theory test last time
	Hold
};
```

You can know the status of the student by summoning the signature function `GetTheoryStatus(BE.Trainee)` that is in the BL layer.
This allows the examiner to rate the student on each criterion on one of three levels, similar to a real test (which also has 3 levels: good, bad, and control appropriation), rather than just 2 levels (True or False).
The second enum variable we created is the Score variable, which expresses the student's abilities in any criterion measured in the test:
```c#
public  enum  Score
{
	// Bad grade, the student does not master this criterion at all
	Bad = 0,

	// A reasonable grade, the student shows partial control of this criterion
	OK,

	// A good grade, the student shows complete control of this criterion
	Good
}
```
This allows the examiner to grade the student in each criterion on one of three levels, similar to a real test (which also has 3 levels for each measure: good, bad, and control appropriation), rather than just 2 levels (`True` or `False`).
