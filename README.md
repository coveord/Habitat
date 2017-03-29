# Sitecore Habitat

Habitat is a Sitecore solution example built on a modular architecture.
The architecture and methodology focuses on:

* Simplicity - *A consistent and discoverable architecture*
* Flexibility - *Change and add quickly and without worry*
* Extensibility - *Simply add new features without steep learning curve*

For more information, please check out the [Habitat Wiki](https://github.com/Sitecore/Habitat/wiki)

## Differences from the [original repo](https://github.com/Sitecore/Habitat)

### Website search
This version of Habitat is using [Coveo for Sitecore](http://www.coveo.com/en/solutions/coveo-for-sitecore) for website search.

Coveo for Sitecore is an Enterprise-grade, Sitecore search provider created by Coveo. It is made for website search features exclusively. It uses advanced ranking algorithms mixed with Machine Learning technology to return the most relevant search results to every visitor, every time.

### Branches

The default branch in this repository is `coveo`. The `master` branch is only used to synchronize with the original habitat repository.

## Installation

> **Please note** that the project assumes the following settings to allow the use of the original Habitat repository side by side with the Habitat.Coveo repository:
>
> *Source location:* C:\projects\Habitat.Coveo\
> *Website location:* C:\websites\Habitat.Coveo.dev.local\
> *Website URL:* http://habitat.coveo.dev.local/
>
> To change these settings see the optional step #2 below

**Important!:** Always run your Visual Studio or Command Line with elevated privileges or *As Administrator*

Please check the [Habitat Resources](https://github.com/Sitecore/Habitat/wiki/02-Resources) page for the tools needed

1. Clone this repository to your local file system.
2. *(optional)* Configure your settings if you are using settings other than the defaults above. To change the standard location of the source files, website files and website URL, modify the following files:
      * **Please be aware to include or omit trailing slashes - as per the default**
      * `/Solution Items/src/Project/Habitat/code/App_Config/Include/Project/z.Habitat.DevSettings.config`
      * `/Solution Items/gulp-config.js`
      * `/Solution Items/publishsettings.targets`
3. Set up a clean Sitecore install with the settings from the previous step
    * We recommend using [Sitecore Instance Manager](https://marketplace.sitecore.net/Modules/S/Sitecore_Instance_Manager.aspx) for the install.
    * Please note that the Sitecore executable installer does not support periods in the domain name and therefore if you are installing using this, please change the default domain (see step 2).
    * Habitat requires:
        * [**Sitecore Experience Platform 8.2 Update-1 (rev. 161115)**](https://dev.sitecore.net/Downloads/Sitecore_Experience_Platform/82/Sitecore_Experience_Platform_82_Update1.aspx) in Experience Platform mode.
            * Installing any other version of Sitecore will lead to errors at synchronization or run time.
        * [**Webforms for Marketers 8.2 Update-1 (rev. 161129)**](https://dev.sitecore.net/Downloads/Web_Forms_For_Marketers/82/Web_Forms_For_Marketers_82_Update1.aspx) module.
            * It should be installed prior to running Sync Unicorn gulp task.
            * Also you need to have running MongoDB instance when installing WFFM module, otherwise it may lead to never ending installation dialogue window.
            * Installing any other version of WFFM will lead to errors at synchronization or run time.
    * Habitat.Coveo requires:
        * [**Coveo for Sitecore 4.0.1088 (March 2017 release) for Sitecore 8.2**](http://download.coveo.com/download/Sitecore/4.0.1088/x64/Coveo%20for%20Sitecore%2082%204.0%20(1088).zip) module.
            * Notes:
                * It should be installed prior to running Sync Unicorn gulp task.
                * Also you need to have running MongoDB instance when installing Coveo for Sitecore module, otherwise it may lead to never ending installation dialogue window.
                * Installing any other version of Coveo for Sitecore will lead to errors at synchronization or run time.
            * Follow the [Coveo for Sitecore installation guide](https://developers.coveo.com/x/yYAkAg)
                * In the post installation wizard:
                    * Create a Coveo Cloud trial organization. It is valid for 30 days.
                    * In the "Configure body indexing options" page, choose the "Index rendered HTML" option.
                    * In the "Activate Coveo for Sitecore" page, endure the "Automatically rename the files" checkbox is checked.
                * Restart both the client and server after the installation.
                * **DO NOT** rebuild the Coveo search indexes after installation.
                * **DO NOT** remove the Full Page XHTML Validation Rule. It is already done by Habitat.
4. Restore Node.js modules
    * Make sure you have version 4+ of node.js [Download here](https://nodejs.org/)
    * In an elevated command window run **`npm install`** in the root of repository.
5. Build and publish the solution
    * Open an command window with elevated privileges and run **`gulp`** in the root of repository.
    * Alternatively:
        * Open **Visual Studio 2015** in *Administrator Mode*
        * Open the **Visual Studio 2015** Task Runner Explorer pane (View | Other Windows | Task Runner Explorer).
        * Switch to "Solution 'Habitat'"
        * Run the "default" task
6. [Rebuild the `coveo_*_index` indexes](https://developers.coveo.com/display/SitecoreV4/Rebuilding+Your+Search+Indexes)
7. Enjoy!