# Driving test management system
## Introduction
The software is designed to allow a driving student to register for the test without being dependent on their teacher.
The student selects the date, time, and starting position at which he wants the test, and the software finds him a suitable tester from the pool of available testers according to the constraints requested by the student.

## Description of screens and system activity
### Screen Flow Diagram
![enter image description here](https://github.com/nmanor/Driving-test-management-system-2019/blob/master/Screenshots/Screen%20flow.png?raw=true)
### The main screen
The main screen is divided into 3 areas:

 1. The student area, where you can register as a new student or login as a student who is already enrolled in the system.
2. The tester area, where you can register as a new tester or login as a tester already registered in the system.
3. The system worker area, where you can connect with a pre-recognized password as a system employee who has control over everything.
Both login as a student and login as examiner, you can view the password itself (instead of the ● marks) by pressing the "show password" button. In addition, you can login by pressing the enter key and not necessarily by pressing the "sign in" button).
![enter image description here](https://github.com/nmanor/Driving-test-management-system-2019/blob/master/Screenshots/Main%20Screen.png?raw=true)

### Student screen
The student screen can be in one of two modes:
1. A condition where the student has already passed a theory test and can register for the test and watch previous tests
2. A situation in which the student has not yet passed a theory, and then the student can take a test and / or update his / her personal information

#### "Update details" tab

This screen is the same for a student who has passed a theory test and for a student who has not yet passed. In this screen the student can update his or her personal details and even delete himself from the system.
The student can enter the number of lessons he or she does or download and upload by pressing the + and - buttons.
![enter image description here](https://github.com/nmanor/Driving-test-management-system-2019/blob/master/Screenshots/Update%20info.png?raw=true)
#### "Theory Test" tab
This screen allows the student to perform a theory test based on predefined random theory questions stored in the system, for a limited time. If the student has passed, he or she will be able to register immediately for the test and if he does not have to wait 24 hours until the next test is taken.
The student can move from question to question, or skip to a specific question by clicking on the question number on the top left of the screen (in the second image).
![enter image description here](https://github.com/nmanor/Driving-test-management-system-2019/blob/master/Screenshots/Theary%20test%201.png?raw=true)
![enter image description here](https://github.com/nmanor/Driving-test-management-system-2019/blob/master/Screenshots/Theary%20test%202.png?raw=true)
#### "Register for Test" tab
In this screen (which appears only in a student who has passed theory) you can register for the test according to the constraints chosen by the student. At the end of the process, the student will receive an email detailing the test details and will be offered the option to add the test to their Google Calendar with all the details of the test along with the date of the test and other information.
By clicking the "Set the start point as my home" button, the student can set the address as his or her home address with which he or she is registered, rather than typing it himself.
![enter image description here](https://github.com/nmanor/Driving-test-management-system-2019/blob/master/Screenshots/Test%20registration.png?raw=true)
![enter image description here](https://github.com/nmanor/Driving-test-management-system-2019/blob/master/Screenshots/Test%20registration%20calander.png?raw=true)
![enter image description here](https://github.com/nmanor/Driving-test-management-system-2019/blob/master/Screenshots/Test%20registration%20email.png?raw=true)
#### The "Your Tests" tab
This screen is divided into 3 sections:
1. Tests where the student has passed, where he can view all the details of all the tests on all types of vehicles in which he has passed a test. A mouse pass on a test that the student passed will have a button that allows him to run an algorithm that creates a license fee specifically tailored to him and containing his identity number, his name, the type of vehicle he passed the test and the expiration date of the fee, which is set exactly 3 months from the date he passed. test. With this bag, the student can go to the mail and pay it, much like a real toll. The resulting fee looks like this:
![enter image description here](https://github.com/nmanor/Driving-test-management-system-2019/blob/master/Screenshots/Fee.png?raw=true)
2. The tests where the student fails. Same as the test view he passed but will show him the details of the tests he failed.
3. Future Tests, where the tests that have not yet been and / or the tests on which the examiner has not yet entered the test score will be displayed, and delete a test if he wishes to cancel it.