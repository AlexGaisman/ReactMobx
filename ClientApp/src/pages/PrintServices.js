import React, { useContext } from 'react';
import { Link } from 'react-router-dom';
import { AuthContext } from './../context/AuthContext';
import ThreeDPrinting from './../images/3DPrinting.png'
import TShirtPrinting from './../images/TShirtPrinting.jpg'
import './PrintServices.css';

import { inject, observer } from 'mobx-react';

//@inject((stores) => ({
//    activityStore: stores.activityStore
//}))


const PrintServices = inject("printingStore")(observer(({ printingStore}) => {

    return (
        <>
            <div className="w-full top-0 bg-white px-10 py-5">

                <div className="flex items-center">
                    <Link
                        to="/signup"
                        className="text-blue-700 mr-6 boxItem"
                    >
                        <p> 3D  Printing </p>
                        <img
                            className="w-32 h-full imageItem"
                            src={ThreeDPrinting}
                            alt="3DPrinting"
                        />
                    </Link>.
                    <Link
                        to="/signup"
                        className="text-blue-700 mr-6 boxItem"
                    >
                        <p> T-Shirt  Printing </p>
                        <img
                            className="w-32 h-full imageItem"
                            src={TShirtPrinting}
                            alt="3DPrinting"
                        />
                    </Link>.
                    <Link
                        to="/signup"
                        className="text-blue-700 mr-6 boxItem"
                    >
                        <p> T-Shirt  Printing </p>
                        <img
                            className="w-32 h-full imageItem"
                            src={TShirtPrinting}
                            alt="3DPrinting"
                        />
                    </Link>.
                </div>

            </div>
        </>
    );
}));

export default PrintServices;