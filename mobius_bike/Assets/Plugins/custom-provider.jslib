mergeInto(LibraryManager.library, {        
    
	/* FIX FOR GRA STARTING TOO SOON */
	  GP_GameStart : function () {
		  window.GamePush.gp.gameStart();
	  }
});