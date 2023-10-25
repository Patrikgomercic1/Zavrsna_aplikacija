import axios from "axios";

export default axios.create({
    baseURL: "pgomercic3-001-site1.anytempurl.com/api/v1",
    headers: {
        "content-type": "application/json"
    }

});