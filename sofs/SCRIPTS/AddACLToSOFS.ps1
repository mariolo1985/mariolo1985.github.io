param (
   [Parameter(Mandatory=$true)]   
   [string]
   $rootwebUrl

)
#-------------------------------------------
# Global Variables
#-------------------------------------------
Add-PSSnapin "Microsoft.SharePoint.PowerShell" -ErrorAction SilentlyContinue
$scriptDir = Split-Path $MyInvocation.MyCommand.Path

$pcaKey = 'PCAGroupName'
$pcaName
$pca

$scaKey = 'PCAReadOnlyGroupName'
$scaName
$sca

$shipmgrKey = 'SofsShippingManagerGroupName'
$shipmgrName
$shipmgr

#-------------------------------------------
# General Functions
#-------------------------------------------

function run([string]$url){
	
	[Microsoft.SharePoint.SPSecurity]::RunWithElevatedPrivileges({
		write-transcript($rootweb);
		$rootweb = GET-SPWEB -IDENTITY $url;	
		$webapp = GET-SPWEBAPPLICATION -IDENTITY $url;
		
		#---------- GET GROUP NAMES ----------
		$pcaName = $webapp.Farm.Properties[$pcaKey];
		$scaName = $webapp.Farm.Properties[$scaKey];
		$shipmgrName = $webapp.Farm.Properties[$shipmgrKey];
			
		if ($pcaName -ne $null){
			$pca = $rootweb.ensureuser($pcaName);
			write-host $pca.Name;
		}	
		if ($scaName -ne $null){
			$sca = $rootweb.ensureuser($scaName);
			write-host $sca.Name;
		}
		if ($shipmgrName -ne $null){
			$shipmgr = $rootweb.ensureuser($shipmgrName);
			write-host $shipmgr.Name;
		}
			
		if (($pca -ne $null) -and ($sca -ne $null) -and ($shipmgr -ne $null)){
			$webapp.sites | %{			
				if ($_.url -like '*sites*'){
					write-host 'Processing site - ' $_.url -ForegroundColor green;				
					
					$visitorsGroup = $_.RootWeb.AssociatedVisitorGroup;		
					if ($visitorsGroup -ne $null){					

						write-host 'Visitors Group Found - ' $visitorsGroup.Name -ForegroundColor yellow;	
						
						try{
							$sca2 = $_.RootWeb.ensureuser($sca.loginname);
							$visitorsGroup.AddUser($sca2);
							$visitorsGroup.Update();

							$pca2 = $_.RootWeb.ensureuser($pca.loginname);
							$visitorsGroup.AddUser($pca2);
							$visitorsGroup.Update();

							$ship2 = $_.RootWeb.ensureuser($shipmgr.loginname);
							$visitorsGroup.AddUser($ship2);
							$visitorsGroup.Update();
						}catch{
							write-error $_.Exception.Message
						}									
					
						write-host $visitorsGroup.Name ' updated';		
											
					}
					
				}
				
			}
			
		
		}
		Stop-Transcript -ErrorAction SilentlyContinue	
		
	});
}

function Write-Transcript([string]$message) {
 
        if (!(Test-Path (Join-Path $ScriptDir "Logs"))) {
        	New-Item (Join-Path $ScriptDir "Logs") -type directory
        }        
        $LogFile = JOIN-PATH $scriptDir '/Logs/AddACL.txt'
        Start-Transcript -path $LogFile -Force -Append
		#$message        
		
}


#-------------------------------------------
# EXECUTOR 
#-------------------------------------------
run($rootwebUrl);