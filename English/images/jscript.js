function menu(tablename,j){
var kk;
for(i=1;i<=j;i++)
{
kk=document.all("menu"+i);
if(tablename==kk)
	{
		if (tablename.style.display=="none" || tablename.style.display==""){tablename.style.display="block";}
		else if (tablename.style.display=="block"){tablename.style.display="none";}
	}
else
	{
		kk.style.display="none";
	}
}
}

function menua(tablename,j){
var kk;
for(i=1;i<=j;i++)
{
kk=document.all("menua"+i);
if(tablename==kk)
	{
		if (tablename.style.display=="none" || tablename.style.display==""){tablename.style.display="block";}
		else if (tablename.style.display=="block"){tablename.style.display="none";}
	}
else
	{
		kk.style.display="none";
	}
}
}