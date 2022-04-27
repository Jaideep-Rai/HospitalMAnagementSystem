import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { HomeComponent } from './views/home/home.component';
import { HeaderComponent } from './Component/header/header.component';
import { MedicineComponent } from './views/medicine/medicine.component';
import { SidebarComponent } from './Component/sidebar/sidebar.component';
import { CreateMedecineComponent } from './views/medicine/create-medecine/create-medecine.component';
import { BillComponent } from './views/bill/bill.component';
import { ApiService } from './services/api.service';
import { CreateBillComponent } from './views/bill/create-bill/create-bill.component';
import { InventoryComponent } from './views/inventory/inventory.component';
import { CreateStockComponent } from './views/inventory/create-stock/create-stock.component';
import { LogInPageComponent } from './Component/log-in-page/log-in-page.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    HeaderComponent,
    MedicineComponent,
    SidebarComponent,
    CreateMedecineComponent,
    BillComponent,
    CreateBillComponent,
    InventoryComponent,
    CreateStockComponent,
    LogInPageComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'medicines', component: MedicineComponent },
      { path: 'medicines/add', component: CreateMedecineComponent },
      { path: 'bills', component: BillComponent },
      { path: 'bills/add', component: CreateBillComponent },
      { path: 'inventory', component: InventoryComponent },
      { path: 'inventory/add', component: CreateStockComponent },
      { path: 'loginpage', component: LogInPageComponent },
    ])
  ],
  providers: [ApiService, AppComponent],
  bootstrap: [AppComponent]
})
export class AppModule { }
