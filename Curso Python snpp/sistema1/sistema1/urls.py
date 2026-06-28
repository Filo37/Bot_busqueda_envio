from django. contrib import admin 
from django. urls import path 
from .views import *
urlpatterns = [
    path('admin/', admin.site.urls),
    path('saludo/', saludar),
    path('serie/', fibonacci_hasta_100),
    path('saldo/', get_saldo),
    ]