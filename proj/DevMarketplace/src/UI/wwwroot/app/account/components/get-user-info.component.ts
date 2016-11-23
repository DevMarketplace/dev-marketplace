import { Component } from '@angular/core';

@Component({
    selector: 'account-user-info',
    templateUrl: './templates/get-user-info.component.html'
})

export class GetUserInfoComponent
{
    firstName: string;
    email: string;
    lastName: string;
}