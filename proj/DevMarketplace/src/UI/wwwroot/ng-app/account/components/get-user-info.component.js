//import { Component, ElementRef, OnInit, AfterViewChecked } from "@angular/core";
//import { AccountService } from "../services/account.service";
//import { CurrentUser } from "../models/current-user.model";
//declare var $: any;
//@Component({
//    selector: "account-user-info",
//    templateUrl: "/angular/template?name=UserAccountMenuPartial",
//    providers: [AccountService]
//})
//export class GetUserInfoComponent implements OnInit, AfterViewChecked {
//    private user: CurrentUser = new CurrentUser();
//    constructor(private accountService: AccountService, private elementRef: ElementRef) {}
//    ngOnInit(): void {
//        this.accountService.getCurrentUser().subscribe(
//            (userResponse: CurrentUser) => { this.user = userResponse; },
//            (error: any) => console.log(<any>error));
//    }
//    ngAfterViewChecked(): void {
//        $(this.elementRef.nativeElement).find(".dropdown-button").dropdown({ hover: false, belowOrigin: true });
//    }
//} 
