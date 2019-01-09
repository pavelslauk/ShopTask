import { Component, OnInit } from '@angular/core';
import { Router, Event, GuardsCheckEnd } from '@angular/router';
     
@Component({
    selector: 'order',
    templateUrl: './order.component.html'
})
export class OrderComponent implements OnInit { 

    constructor(private router: Router) { 
        router.events.subscribe((event: Event) => {
            if (event instanceof GuardsCheckEnd)
            {
                console.log(event);
                if (!event.shouldActivate) {
                    router.navigateByUrl('/shoptask/Order');
                }
            }
        })
    }

    ngOnInit() { }  
}