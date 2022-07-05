import React, { useContext, useEffect } from 'react';
import { observable } from 'mobx';


export const colors = observable({
    test: 12
});

//const ActivityStore = observable({
//    test: 12
//});

const ActivityStore = () => {
     const colors = observable({
        test: 12
    });

  // @observable myTest = false;
    const title = "Hello from Mobx!";

    useEffect(() => {
        var t = 12;
    });
}

export default ActivityStore;