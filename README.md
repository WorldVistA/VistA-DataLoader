For how to use the VistA Dataloader, please check out the user guide under the documentation directory.

VistA Loader allows user to create patient medical records on an Excel spreadsheet and import them into VistA EHR. The software built from this repository is designed to work with Plan VI versions of VistA. Hence, it will be able to import localized patient data. A example Korean patient medical record Excel file is included in the documentation subdirectory.

The VistA Loader has two components: the C# VistA DataLoader client and the MUMPS ISI DATA IMPORT routines. You will need to rebuild the C# VistA DataLoader project, which will create a installer to install VistA DataLoader on Windows.

Here are steps to install the software (the following steps assumes that you will be using a Windows 10 environment with Docker installed: First, clone the repository on your local drive,i.e., 

git clone https://github.com/vista-dataloader.git

1. Install the VEHU VistA instance based on the Plan 6 docker image from the OSEHRA docker hub.
2. Install the VistADataLoader.KID (located in the VistA subdirectory) on the running VistA
3. Setup a user, i.e., fakedoc1 so that it will have access to the ISI DATA IMPORT routines SECONDARY MENU OPTIONS ISI DATA IMPORT
4. Install Visual Studio community version (2017).
5. Install MSI Installer plugin for Visual Studio - Download from https://marketplace.visualstudio.com/items?itemName=visualstudioclient.MicrosoftVisualStudio2017InstallerProjects
6. From Visual Studio, open the Data Loader.sln solution. Rebuild all of the projects.
7. Assuming no errors, rebuild the DataLoaderInstaller project. Depending on whether you are building for debug or release versions, you will find an Windows installer under the DataLoaderInstall/bin directory.
8. Install and run the VistA DataLoader client.
9. Configure the Excel spreadsheet template with the test patient records.
10. From the VistA DataLoader client, connect to VistA. Open the spreadsheet and import the test patient to VistA.
11. Check the error log to see what data elements are imported successful or failed. Failure are usually caused by misconfiguration.
