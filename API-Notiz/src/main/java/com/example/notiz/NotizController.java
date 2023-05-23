package com.example.notiz;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;
import java.util.Optional;

@RestController
@CrossOrigin(origins = "http://127.0.0.1:5500")
public class NotizController {
    @RequestMapping("/home")
    public String serviceTest(){
        return "Das Service funktioniert!";
    }

    @Autowired
    private NotizService notizService;

    @RequestMapping("/notizen")
    public List<Notiz> getallNotizen() {
        return notizService.getallNotizen();
    }

    @RequestMapping("/notizen/{id}")
    public Optional<Notiz> getNotiz(@PathVariable String id) {
        return notizService.getNotiz(id);
    }

    @RequestMapping(method= RequestMethod.POST, value="/notiz")
    public String addNotiz(@RequestBody Notiz notiz) {
        notizService.addNotiz(notiz);

        String response = "{\"success\": true, \"message\": Notiz has been added successfully.}";
        return response;
    }

    @RequestMapping(method=RequestMethod.PUT, value="/notiz/{id}")
    public String updateNotiz(@RequestBody Notiz notiz, @PathVariable String id) {
        notizService.updateNotiz(id, notiz);
        String response = "{\"success\": true, \"message\": Notiz has been updated successfully.}";
        return response;
    }

    @DeleteMapping(value="/notiz/{id}")
    public String deleteNotiz(@PathVariable String id) {
        notizService.deleteNotiz(id);
        String response = "{\"success\": true, \"message\": Notiz has been deleted successfully.}";
        return response;
    }
}
