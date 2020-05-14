<div style="direction:rtl; text-align: justify;">
<h5>مقدمه</h5>


هدف از ساخت یک میان افزار در سیستم های توزیع شده این است که انتزاعی ایجاد کنیم که مجموعه ای از کامپیوترهای مستقل از هم به صورت یک کامپیوتر یکپارچه و قدرتمند دیده شوند. یک میان افزار با ایجاد انواع مختلفی از شفافیت این امکان را به برنامه کاربردی می دهد تا بدون درگیر  شدن با جزئیات، از منابع تحت مدیریت میان افزار استفاده کند (منابعی مانند
CPU، حافظه اصلی و جانبی و غیره).

میان افزار های تجاری مختلفی در بازار موجود است. برخی از آن ها کاملا حرفه ای و دارای رابط برنامه نویسی برنامه کاربردی پیچیده ای می باشند. هدف ما از ساخت این نرم افزار این بود که اولا میان افزاری ایجاد کنیم که در عین سهولت استفاده و سادگی، قابلیت انعطاف زیادی نیز داشته باشد تا بتواند سناریوهای مختلف محاسباتی را پشتیبانی کند. ثانیا تحت معماری .NET Framework باشد.

میان افزاری که ما طراحی کرده و ساخته ایم، اساسا دو نوع شفافیت را ایجاد می کند. مورد اول، شفافیت در دسترسی به منابع کامپیوترهای دیگر است (Access Transparency). مورد دوم شفافیت در از کار افتادگی کامپیوترهایی است که تحت نظر میان افزار کار می کنند (Failure Transparency). اکثر رویدادهایی که در سطوح داخلی میان افزار یا در سطح شبکه رخ می دهد، به صورت گزارش (Log) در اختیار نرم افزار کاربردی که از میان افزار استفاده می کند قرار داده می شود، البته هیچ الزامی وجود ندارد که نرم افزار کاربردی از این اطلاعات استفاده کند.
به منظور اینکه در ادامه به آسانی به نرم افزاری که ساخته ایم ارجاع داشته باشیم، آن را "Quax"  نام گذاری کرده ایم.


</div>