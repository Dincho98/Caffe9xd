 <?php 
$con = mysql_connect('ponudehrcom1.ipagemysql.com', 'drazen_vuk', 'drazen2814mia'); 
if (!$con) { 
    die('Could not connect: ' . mysql_error()); 
} 
 
mysql_select_db('evidencija',$con); 
//date_default_timezone_set("Europe/Zagreb");
?> 