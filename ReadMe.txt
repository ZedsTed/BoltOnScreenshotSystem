Bolt-On Screenshot System (B.O.S.S.) v2.0
==
This plugin allows you to take screenshots within KSP at a higher resolution than your current screen resolution, using something called 'supersampling'. 

Changelog
--
v2.1.2 - Fixed up the issues with hiding UI, made it persistent and changed it to the p key.
v2.1.1 - Fixed an issue in the paths given to the screenshot method. Screenshots should actually save now...
v2.1 - Added in the ability to hide the BOSS UI with the F2 key.
v2.0 - Updated for 0.23 compatibility. Refactored and removed a lot of code; mainly to do with requiring a part and then method of text field input.
v0.2.3 - Updated for 0.21 compatibility.
v0.2.2 - Fixed issues with multiple Bolts being on the same craft and thus causing BOSS to bug out.

v0.2.1 - Reverted to old internal structure for GUI, will progress with it at another date.

v0.2 - Changed to PartModule. Updated for 0.18. Changed GUI. Fixed various little bugs.

v0.1.2 - Prevented spamming of the debug log. Fixed non-updating text field.

v0.1.1 - Added in Settings Persistence.

v0.1 - Public Release

Licence
--
This plugin is licenced under the GPLv3. You may obtain a copy of this licence at https://www.gnu.org/licenses/gpl-3.0.txt or read the one included in this plugin. The Add-On as a whole is licenced under the Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported (CC BY-NC-SA 3.0), a copy of which can be found here - https://creativecommons.org/licenses/by-nc-sa/3.0/. 

Acknowledgements
--
A really big thanks to the following people for their massive help in getting me started with this plugin and C#: r4m0n, The_Duck, cybutek, Innsewerants and careo especially for all that he has done.

FAQ
--
Why is this called v2.0? Where was v1.0?
Version 1.0 would have been released around 0.22, but I never managed to hammer out some of the more persistent bugs with combining BXSS and BOSS, therefore I decided to scrap that and go for a refactor of BOSS, ignoring most of BXSS for the moment.

I keep getting a Null Ref when starting up KSP and it seems to tie to BOSS. Why?
The Null Ref occurs when the Addon Loader tries to load up BOSS and only occurs when I set it to load in every scene. It seems mostly harmless for the moment and only means that the loader loads BOSS again until it doesn't null ref - about twice.
Working to fix it, however.


Misc.
--
Forum thread: http://forum.kerbalspaceprogram.com/showthread.php/34631
