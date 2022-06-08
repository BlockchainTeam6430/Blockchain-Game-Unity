mergeInto(LibraryManager.library, 
{
	EELogin: async function()
	{
		ELogin();
	},

	EEGetWalletName: async function()
	{
		var returnStr = EGetWalletName();
    		var bufferSize = lengthBytesUTF8(returnStr) + 1;
    		var buffer = _malloc(bufferSize);
    		stringToUTF8(returnStr, buffer, bufferSize);
    		return buffer;
	},

	EEAddMoney: async function(amount)
	{
		EAddMoney(amount);
	},

	EEWithdrawMoney: async function(amount)
	{
		EWithdrawMoney(amount);
	},

	EEGetAmountOfMoney: async function()
	{
		return EGetAmountOfMoney();
	},

	EEPVPModeEntered: async function()
	{
		EPVPModeEntered();
	},

	EEPVPModeIsReady: async function()
	{
		return EPVPModeIsReady();
	},

	EEPostKey: async function(key)
	{
		return EPostKey(key);
	},

	EECheckIfPosted: async function()
	{
		return ECheckIfPosted();
	},
});