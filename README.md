Silent Hunter
=============

The rise in illegal P2P applications on the UCT network is cause for alarm, especially when students start abusing the lab machines for their own purposes. Silent Hunter is an application that has been written to run in the background on a lab machine and kill any recognisable P2P application instance should it be started up. 

Processes are stopped by checking running process names against a wishlist of processes to be killed.

Silent Hunter also logs all kills with the Commerce webserver, attempting to log time of instance, current Novell user, the process killed, the machine's primary IP and MAC addresses as well as the machine's name.

Note: To avoid detection, Silent Hunter frequently changes it's launch name and to avoid tinkering, checks against an online process list.

Created by Craig Lotter, October 2007

*********************************

Project Details:

Coded in Visual Basic .NET using Visual Studio .NET 2005
Implements concepts such as windows process manipulation, threading and web page interaction.
Level of Complexity: Simple

*********************************

Update 20080305.03:

- Now scans for associated folder structure to identify DC++ clients who might have had their executable name changed
- Updated process list
- Increased polling time due to new longer search algorithms

*********************************

Update 20080307.04:

- Randomly generate file names on startup
- Split file search and process search on separate threads
- Decrease polling time for process search
- Stop generating activity and error logs
- clean up resource files

*********************************

Update 20080416.05:

- Bug Fix: removed certain system folders as allowable host directories for Silent Hunter to operate from
- Startup folder and executable are now both hidden
