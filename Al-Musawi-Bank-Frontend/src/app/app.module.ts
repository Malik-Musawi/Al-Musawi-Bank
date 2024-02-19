import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms'; // Import FormsModule here


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { NavbarComponent } from './navbar/navbar.component';
import { ServicesComponent } from './services/services.component';
import { ContactComponent } from './contact/contact.component';
import { SignInComponent } from './sign-in/sign-in.component';
import { SignUpComponent } from './sign-up/sign-up.component';
import { AccountOverviewComponent } from './account-overview/account-overview.component';
import { AboutUsComponent } from './about-us/about-us.component';
import { TransactionsComponent } from './transactions/transactions.component';
import { DepositComponent } from './deposit/deposit.component';
import { TransferComponent } from './transfer/transfer.component';
import { FooterComponent } from './footer/footer.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { UserService } from './Auth/user.service';
import { AuthGuard } from './Auth/auth.guard';


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NavbarComponent,
    ServicesComponent,
    ContactComponent,
    SignInComponent,
    SignUpComponent,
    AccountOverviewComponent,
    AboutUsComponent,
    TransactionsComponent,
    DepositComponent,
    TransferComponent,
    DashboardComponent,
    FooterComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    FormsModule // Add FormsModule to the imports array
  ],
  providers: [AuthGuard],
  bootstrap: [AppComponent]
})
export class AppModule { }
