import { Component, OnInit } from '@angular/core';
import { AccountService } from '../Auth/account.service';
import { TransactionService } from '../Auth/transaction.service';
import { UserService } from '../Auth/user.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  accounts: any[] = [];
  transactions: any[] = [];
  totalBalance: number = 0;

  constructor(
    private accountService: AccountService,
    private transactionService: TransactionService,
    private userService: UserService
  ) {}

  ngOnInit() {
    const userId = this.userService.getCurrentUserId();
    if (userId) {
      this.fetchAccounts(userId);
    }
  }
  
  fetchAccounts(userId: number) {
    this.accountService.getAccounts(userId).subscribe(accounts => {
      this.accounts = accounts;
      this.totalBalance = accounts.reduce((acc, account) => acc + account.balance, 0);
      // Fetch transactions after accounts have been loaded
      accounts.forEach(account => {
        this.fetchTransactions(account.id);
      });
    }, error => {
      console.error('Failed to fetch accounts:', error);
    });
  }
  
  fetchTransactions(accountId: number) {
    this.transactionService.getTransactionsForAccount(accountId).subscribe(transactions => {
      this.transactions = [...this.transactions, ...transactions];
    }, error => {
      console.error('Failed to fetch transactions for account:', accountId, error);
    });
  }
  
  changePassword(oldPassword: string, newPassword: string) {
    this.userService.changePassword(oldPassword, newPassword).subscribe(response => {
      // Handle the response, possibly showing a success message
    });
  }
}
